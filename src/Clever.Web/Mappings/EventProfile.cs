using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventDTO, Event>();
        CreateMap<Event, EventDetailDTO>();
    }
}