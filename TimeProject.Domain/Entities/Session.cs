using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Domain.Entities;

public class Session
{
    public int SessionId { get; set; }
    public SessionType Type { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? From { get; set; } = string.Empty;

    public int RecordId { get; set; }
    public int? UserId { get; set; }

    public Record? Record { get; set; }
    public Category? Category { get; set; }
    public IEnumerable<Period>? Periods { get; set; }
}