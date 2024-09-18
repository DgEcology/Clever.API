using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings
{
    public class OrganiserProfile : Profile
    {
        public OrganiserProfile()
        {
            CreateMap<OrganiserApplicationDTO, OrganiserApplication>()
            .ForSourceMember(src => src.Photo,
            opt => opt.DoNotValidate());
        }
    }
}