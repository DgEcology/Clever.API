namespace Clever.Web.DTO;

public record EventDTO
{
    public string Title { get; set; }

    public string Description { get; set; }

    public IFormFile Image { get; set; }

    public string Geolocation { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

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
    string UserId,
    long ReactionId,
    long AttendanceId
);