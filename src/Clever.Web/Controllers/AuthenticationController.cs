using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Controllers;

[ApiController]
[Route("[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthenticationController
    (
        IConfiguration configuration,
        IMapper mapper,
        UserManager<User> userManager
    )
    {
        this._configuration = configuration;
        this._mapper = mapper;
        this._userManager = userManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Signup(SignupDTO signupDTO)
    {
        User user = _mapper.Map<User>(signupDTO);
        var result = await _userManager.CreateAsync(user, signupDTO.Password!);
        if (result.Succeeded)
        {
            return StatusCode(StatusCodes.Status201Created, $"User '{user.UserName}' has been created");
        }
        else
        {
            return StatusCode(StatusCodes.Status400BadRequest, $"Error: {String.Join(" ", result.Errors.Select(e => e.Description))}");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email!);
        if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password!))
        {
            return StatusCode(StatusCodes.Status401Unauthorized, "Invalid login attempt");
        }
        else
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!)),
                SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            };
            claims.AddRange((await _userManager.GetRolesAsync(user))
                .Select(role => new Claim(ClaimTypes.Role, role)));
            var jwtObject = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signingCredentials
            );
            var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);
            CookieOptions options = new CookieOptions()
            {
                Domain = "https://eco.kostyazero.com/",
                Path = "/",
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            Response.Cookies.Append("SESSION", $"Bearer {jwtString}", options);
            return StatusCode(StatusCodes.Status200OK, jwtString);
        }
    }    
}