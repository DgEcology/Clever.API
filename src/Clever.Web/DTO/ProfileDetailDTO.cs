namespace Clever.Web.DTO
{
    public record ProfileDetailDTO
    (
         string UserName,
         string About,
         int Age,
         string Email,
         string PhoneNumber,
         int Points 
    );
}