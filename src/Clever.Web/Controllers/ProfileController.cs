using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Clever.Web.DTO;
using Clever.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Clever.Web.Controllers
{
    [ApiController]
    [Route("profile")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<User> userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProfileDetailDTO>> GetProfile()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            return _mapper.Map<ProfileDetailDTO>(await _userManager.FindByNameAsync(name!));
        }

        [HttpGet("/points")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> GetPoints()
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            User user = await _userManager.FindByNameAsync(name);
            return user!.Points;
        }
    }
}