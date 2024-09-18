using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings
{
    public class OrganisationProfile : Profile
    {
        public OrganisationProfile()
        {
            CreateMap<OrganisationDTO, OrganisationProfile>()
            .ForSourceMember(src => src.Photo,
            opt => opt.DoNotValidate());
        }
    }
}