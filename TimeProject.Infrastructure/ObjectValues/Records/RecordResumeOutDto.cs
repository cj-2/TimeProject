namespace TimeProject.Infrastructure.ObjectValues.Records;

public class RecordResumeOutDto
{
    public string Formatted { get; set; }
    public double Seconds { get; set; }
    public int Count { get; set; }
    public DateTimeOffset? FirstDate { get; set; }
    public DateTimeOffset? LastDate { get; set; }
}