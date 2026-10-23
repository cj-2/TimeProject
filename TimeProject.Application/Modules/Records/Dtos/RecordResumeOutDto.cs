using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Records.Dtos;

public class RecordResumeOutDto
{
    public string Formatted { get; init; } = string.Empty;
    public double Seconds { get; init; }
    public int Count { get; init; }
    public DateTimeOffset? FirstDate { get; init; }
    public DateTimeOffset? LastDate { get; init; }
    
    public static RecordResumeOutDto Map(RecordResume record)
    {
        return new RecordResumeOutDto
        {
            Formatted = record.Formatted,
            Seconds = record.Seconds,
            Count = record.Count,
            FirstDate = record.FirstDate,
            LastDate = record.LastDate
        };
    }
}