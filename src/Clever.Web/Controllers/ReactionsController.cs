using AutoMapper;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;
using Clever.Web.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Clever.Web.Controllers
{
    [Route("/reaction")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReactionRepository _reactionRepository;
        
        public ReactionsController(IMapper mapper, IReactionRepository reactionRepository)
        {
            this._mapper = mapper;
            this._reactionRepository = reactionRepository;
        }

        [HttpPost("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add(long id)
        {
            try
            {
                Reaction reaction = new Reaction();
                reaction.EventId = id;
                reaction.UserId = "Test UserId";
                _reactionRepository.Add(reaction);
                return CreatedAtAction(nameof(GetById), new {id = reaction.Id}, reaction);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getByEventId/{eventId:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReactionDetailDTO>>> GetByEventId(long eventId)
        {
            return (await _reactionRepository.GetByEventIdAsync(eventId)).Select(_mapper.Map<ReactionDetailDTO>).ToList();
        }

        [HttpGet("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReactionDetailDTO>> GetById(long id)
        {
            try
            {
                return _mapper.Map<ReactionDetailDTO>(await _reactionRepository.GetByIdAsync(id));
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

        [HttpDelete("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove(long id)
        {
            try
            {
                _reactionRepository.Remove(id);
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