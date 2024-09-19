using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Clever.Web.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("/administration")]
    public class AdministrationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventRepository _eventRepository;
        private readonly IOrganiserApplicationRepository _organiserApplicationRepository;

        public AdministrationController(UserManager<User> userManager, IEventRepository eventRepository, IOrganiserApplicationRepository organiserApplicationRepository)
        {
            this._userManager = userManager;
            this._eventRepository = eventRepository;
            this._organiserApplicationRepository = organiserApplicationRepository;
        }

        [HttpPut("events/{eventId:long:min(0)}/accept")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AcceptEvent(long eventId)
        {
            try
            {
                await _eventRepository.Accept(eventId);
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ProblemDetails()
                {
                    Status = 404,
                    Title = exception.Message
                });
            }
        }

        [HttpPut("organiserApplications/{applicationId:long:min(0)}/accept")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AcceptApplication(long applicationId)
        {
            try
            {
                await _organiserApplicationRepository.Accept(applicationId);
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                User user = await _userManager.FindByIdAsync(userId);
                await _userManager.AddToRoleAsync(user!, "Organiser");
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(new ProblemDetails()
                {
                    Status = 404,
                    Title = exception.Message
                });
            }
        }
    }
}