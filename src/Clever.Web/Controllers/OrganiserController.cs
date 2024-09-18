using System.Security.Claims;
using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;
using Clever.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clever.Web.Controllers
{
    [ApiController]
    [Route("organiser")]
    public class OrganiserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        private readonly IOrganiserApplicationRepository _repository;

        public OrganiserController(UserManager<User> userManager, IMapper mapper, IOrganiserApplicationRepository repository)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Apply(OrganiserApplicationDTO organiserApplicationDTO)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(name!);
            var organiserApplication = _mapper.Map<OrganiserApplication>(organiserApplicationDTO);
            // TODO Add photo uploading
            organiserApplication.Photo = "/images/21.png";
            organiserApplication.UserId = user!.Id;
            await _repository.Add(organiserApplication);
            return Created();
        }
    }
}