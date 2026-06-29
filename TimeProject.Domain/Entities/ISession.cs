using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Domain.Entities;

public interface ISession
{
    int SessionId { get; set; }
    DateTimeOffset Date { get; set; }
    int UserId { get; set; }
    int? RecordId { get; set; }
    int? CategoryId { get; set; }
    SessionType Type { get; set; }
    string? From { get; set; }
}