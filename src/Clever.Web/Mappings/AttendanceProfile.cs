using AutoMapper;
using Clever.Domain.Entities;
using Clever.Web.DTO;

namespace Clever.Web.Mappings;

public class AttendanceProfile : Profile
{
    public AttendanceProfile()
    {
        CreateMap<Attendance, AttendanceDetailDTO>();
    }
}