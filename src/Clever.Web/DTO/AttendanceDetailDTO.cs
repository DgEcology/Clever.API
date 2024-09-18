namespace Clever.Web.DTO;

public record AttendanceDetailDTO
(
    long Id,
    long EventId,
    string UserId,
    string Status
);
