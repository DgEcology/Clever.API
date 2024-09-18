using System.ComponentModel.DataAnnotations;

namespace Clever.Web.DTO;

public record SignupDTO
{
    [Required]
    [MinLength(7)]
    public string? UserName { get; set; }
    
    [Required]
    [Range(16, 100)]
    public int Age { get; set; }

    [Required]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public string? About { get; set; }

    [Required]
    [MinLength(12)]
    [MaxLength(45)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string? RepeatPassword { get; set; }
}