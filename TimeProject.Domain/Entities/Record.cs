namespace TimeProject.Domain.Entities;

public class Record
{
    public int RecordId { get; init; }
    public string Code { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ExternalLink { get; set; }

    public int? CategoryId { get; set; }
    public int UserId { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public IEnumerable<Period>? Periods { get; init; }
    public Category? Category { get; init; }
    public RecordResume? Resume { get; init; }
}