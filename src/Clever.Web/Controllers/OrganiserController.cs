using System.Security.Claims;
using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;
using Clever.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Clever.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clever.Web.Controllers
{
    [ApiController, Authorize(Roles = "Organiser")]
    [Route("api/organiser")]
    public class OrganiserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ImageManager _imageManager;

        private readonly IMapper _mapper;

        private readonly IOrganiserApplicationRepository _repository;

        public OrganiserController(UserManager<User> userManager, ImageManager imageManager, IMapper mapper, IOrganiserApplicationRepository repository)
        {
            this._userManager = userManager;
            this._imageManager = imageManager;
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
            organiserApplication.Photo = await _imageManager.SaveImageAsync(organiserApplicationDTO.Photo!);
            organiserApplication.UserId = user!.Id;
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetAsync($"https://www.tbank.ru/business/contractor/legal/{organiserApplicationDTO.OrganisationNumber}/");
            if (!result.IsSuccessStatusCode) return Forbid();
            await _repository.Add(organiserApplication);
            return Created();
        }
    }
}