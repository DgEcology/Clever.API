using System.ComponentModel.DataAnnotations;

namespace Clever.Web.DTO;

public record LoginDTO
{
    [Required]
    [EmailAddress]
    public string? Email {get; set;}

    [Required]
    public string? Password {get; set;}
}