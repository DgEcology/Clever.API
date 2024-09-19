using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Clever.Domain.Entities;
using Clever.Domain.Interfaces;
using Clever.Persistence;
using Clever.Persistence.Repositories;
using Clever.Web.Mappings;
using Clever.Web.Services;

namespace Clever.Web;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddDbContext<ApplicationDbContext>();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddAutoMapper(config =>
		{
			config.AddProfile(new EventProfile());
			config.AddProfile(new ReactionProfile());
			config.AddProfile(new UserProfile());
			config.AddProfile(new OrganiserProfile());
			config.AddProfile(new AttendanceProfile());
			config.AddProfile(new UserProfileProfile());
		});

		builder.Services.AddTransient<IEventRepository, EventRepository>();
		builder.Services.AddTransient<IReactionRepository, ReactionRepository>();
		builder.Services.AddTransient<IOrganiserApplicationRepository, OrganiserApplicationRepository>();
		builder.Services.AddTransient<IAttendanceRepository, AttendanceRepository>();

		builder.Services.AddTransient<ImageManager, ImageManager>();

		builder.Services
			.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>();
		
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme =
			options.DefaultChallengeScheme =
			options.DefaultForbidScheme =
			options.DefaultScheme =
			options.DefaultSignInScheme =
			options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidAudience = builder.Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
				};
		});
		builder.Services.AddCors(options =>
		{
    		options.AddDefaultPolicy(builder =>
    		{
        		builder
					.AllowAnyOrigin()
            		.AllowAnyHeader()
            		.AllowAnyMethod();
			});
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		using (IServiceScope scope = app.Services.CreateScope())
        {
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("Organiser"))
            {
                await roleManager.CreateAsync(new IdentityRole("Organiser"));
            }
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
        }

		app.UseExceptionHandler("/error");

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseAuthentication();

		app.UseAuthorization();

		app.UseCors();

		app.MapControllers();

		app.Run();
	}
}
