using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Clever.Web.DTO;
using Clever.Domain.Interfaces;
using Clever.Domain.Exceptions;

namespace Clever.Web.Controllers;

[ApiController]
[Route("/events")]
public class EventsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    
    public EventsController(IMapper mapper, IEventRepository eventRepository)
    {
        this._mapper = mapper;
        this._eventRepository = eventRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EventDetailDTO>>> GetAll()
    {
        return (await _eventRepository.GetAllAsync()).Select(_mapper.Map<EventDetailDTO>).ToList();
    }

    [HttpGet("{id:long:min(0)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDetailDTO>> GetById(long id)
    {
        try
        {
            return _mapper.Map<EventDetailDTO>(await _eventRepository.GetByIdAsync(id));
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