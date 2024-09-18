using AutoMapper;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;
using Clever.Web.DTO;
using Microsoft.AspNetCore.Http;
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
            Reaction reaction = new Reaction();
            reaction.EventId = id;
            reaction.UserId = "Test UserId";
            _reactionRepository.Add(reaction);
            return CreatedAtAction(nameof(GetById), new {id = reaction.Id}, reaction);
        }

        // [HttpGet("getByEventId/{id:long:min(0)}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public async Task<ActionResult<IEnumerable<Reaction>>> GetByEventId(long id)
        // {
        //     return await _reactionRepository.GetByEventIdAsync(id);
        // }

        [HttpGet("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reaction>> GetById(long id)
        {
            try
            {
                return await _reactionRepository.GetByIdAsync(id);
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
        public ActionResult<Reaction> Remove(long id)
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