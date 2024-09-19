using System.ComponentModel.DataAnnotations;

namespace Clever.Web.DTO;

public record EventDTO
{
    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public IFormFile? Image { get; set; }

    [Required]
    public string? Geolocation { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public long TagId { get; set; }
}

public record EventDetailDTO
(
    long Id,
    string Title,
    string Description,
    string Image,
    string Geolocation,
    DateTime StartTime,
    DateTime EndTime,
    DateTime PublishTime,
    bool IsArchived,
    string SecretKey,
    bool IsAccepted,
    long TagId,
    string UserId
);