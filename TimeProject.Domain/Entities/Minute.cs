using TimeProject.Domain.Interfaces;

namespace TimeProject.Domain.Entities;

public class Minute: ITimeSpanCalc
{
    public int MinuteId { get; init; }
    public DateTimeOffset Date { get; init; }
    public int Total { get; init; }

    public int RecordId { get; init; }
    public int SessionId { get; init; }
    public int? UserId { get; set; }

    public DateTimeOffset Start => Date;
    public DateTimeOffset? End => Date.AddMinutes(Total);

    public TimeSpan TimeSpan => TimeSpan.FromMinutes(Total);
    public TimeSpan TimeSpanCut(DateTimeOffset limit) => limit > End ? TimeSpan : limit.Subtract(Start);
}