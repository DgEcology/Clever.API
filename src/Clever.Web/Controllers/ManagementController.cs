using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using QRCoder;
using Clever.Domain.Interfaces;
using Clever.Web.DTO;
using Clever.Domain.Entities;
using Clever.Web.Services;
using Clever.Domain.Exceptions;


namespace Clever.Web.Controllers
{
    [ApiController, Authorize(Roles = "Organiser, Administrator")]
    [Route("api/manageEvents")]
    public class ManagementController : ControllerBase
    {
        private readonly ImageManager _imageManager;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public ManagementController(ImageManager imageManager, IMapper mapper, IEventRepository eventRepository)
        {
            this._imageManager = imageManager;
            this._mapper = mapper;
            this._eventRepository = eventRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(EventDTO eventDTO)
        {
            Event eventEntity = _mapper.Map<Event>(eventDTO);
            eventEntity.SecretKey = Guid.NewGuid().ToString("N");
            eventEntity.Image = await _imageManager.SaveImageAsync(eventDTO.Image!);
            eventEntity.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _eventRepository.Add(eventEntity);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EventDetailDTO>>> GetEvents()
        {
            return (await _eventRepository.GetAllAsync()).Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Select(_mapper.Map<EventDetailDTO>)
                .ToList();
        }

        [HttpGet("{eventId:long:min(0)}/qr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetQRCode(long eventId)
        {
            try
            {
                var secretKey = (await _eventRepository.GetByIdAsync(eventId)).SecretKey;
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode($"eco.kostyazero.com/api/verify/{secretKey}", QRCodeGenerator.ECCLevel.Q))
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    byte[] qrCodeImage = qrCode.GetGraphic(20);
                    return await _imageManager.SaveImageAsync(qrCodeImage!);
                }
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