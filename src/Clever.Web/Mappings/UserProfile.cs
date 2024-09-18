using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<SignupDTO, User>()
            .ForSourceMember(src => src.Password,
                opt => opt.DoNotValidate())
            .ForSourceMember(src => src.RepeatPassword,
                opt => opt.DoNotValidate());
    }
}