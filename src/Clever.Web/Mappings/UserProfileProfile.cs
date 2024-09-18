using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<User, ProfileDetailDTO>();
        }
    }
}