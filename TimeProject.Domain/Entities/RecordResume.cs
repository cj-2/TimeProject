namespace TimeProject.Domain.Entities;

public class RecordResume
{
    public int RecordId { get; set; }
    public string Formatted { get; set; } = string.Empty;
    public double Seconds { get; set; }
    public DateTimeOffset? FirstDate { get; set; }
    public DateTimeOffset? LastDate { get; set; }
    public int Count { get; set; }
    public int UserId { get; set; }
}