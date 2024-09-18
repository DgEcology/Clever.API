using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        private readonly IOrganiserApplicationRepository _organisationApplicationTableRepository;

        public OrganiserController(UserManager<User> userManager, IMapper mapper, IOrganiserApplicationRepository organiserApplicationTableRepository)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._organisationApplicationTableRepository = organiserApplicationTableRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Apply(OrganisationDTO organisationDTO)
        {
            var name = User.FindFirstValue(ClaimTypes.Name);
            
            var user = await _userManager.FindByNameAsync(name);

            string id = user.Id;

            var organiserApplication = _mapper.Map<OrganiserApplication>(organisationDTO);
            organiserApplication.UserId = id;
            _organisationApplicationTableRepository.Add(organiserApplication);

            return StatusCode(StatusCodes.Status200OK); 
        }
        
    }
}