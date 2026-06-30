namespace TimeProject.Domain.Entities;

public class Record
{
    public int RecordId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ExternalLink { get; set; }
    public int? CategoryId { get; set; }
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public IEnumerable<Period>? Periods { get; set; }
    public Category? Category { get; set; }
    public RecordResume? Resume { get; set; }
}