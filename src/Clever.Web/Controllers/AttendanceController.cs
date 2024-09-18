using System.Security.Claims;
using AutoMapper;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;
using Clever.Web.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clever.Web.Controllers
{
    [ApiController]
    [Route("/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEventRepository _eventRepository;

        public AttendanceController(UserManager<User> userManager, IMapper mapper, IAttendanceRepository attendanceRepository, IEventRepository eventRepository)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._attendanceRepository = attendanceRepository;
            this._eventRepository = eventRepository;
        }

        [HttpPost("{eventId:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(long eventId)
        {
            try
            {
                Event eventEntity = await _eventRepository.GetByIdAsync(eventId);
                if (DateTime.Now > eventEntity.StartTime) return BadRequest();
                Attendance attendance = new Attendance()
                {
                    EventId = eventId,
                    UserId = "Test UserId",
                    Status = "Skipped"
                };
                await _attendanceRepository.Add(attendance);
                return CreatedAtAction(nameof(GetById), new {id = attendance.Id}, attendance);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getByEventId/{eventId:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AttendanceDetailDTO>>> GetByEventId(long eventId)
        {
            return (await _attendanceRepository.GetByEventIdAsync(eventId)).Select(_mapper.Map<AttendanceDetailDTO>).ToList();
        }

        [HttpGet("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AttendanceDetailDTO>> GetById(long id)
        {
            try
            {
                return _mapper.Map<AttendanceDetailDTO>(await _attendanceRepository.GetByIdAsync(id));
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

        [HttpPut("/verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Verify(string secretKey)
        {
            try
            {
                Event eventEntity = await _eventRepository.GetBySecretKeyAsync(secretKey);
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                if (eventEntity.EndTime + TimeSpan.FromHours(1) >= DateTime.Now)
                {
                    await _attendanceRepository.MarkAsAttended(eventEntity.Id, userId);
                    // TODO add points to the organiser and to the user
                    return Ok();
                }
                return BadRequest();
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
