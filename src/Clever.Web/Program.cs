using Clever.Domain.Interfaces;
using Clever.Persistence;
using Clever.Persistence.Repositories;
using Clever.Web.Mappings;

namespace Clever.Web;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddDbContext<ApplicationDbContext>();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddAutoMapper(config =>
		{
			config.AddProfile(new EventProfile());
		});

		builder.Services.AddTransient<IEventRepository, EventRepository>();
		builder.Services.AddTransient<IReactionRepository, ReactionRepository>();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseExceptionHandler("/error");

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
