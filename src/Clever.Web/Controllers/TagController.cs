using Clever.Domain.Entities;
using Clever.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clever.Web.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repository;

        public TagController(ITagRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Tag>>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
    }
}