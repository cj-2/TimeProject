using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Infrastructure.ObjectValues.Statistics;

public class RecordRangeProgress
{
    public RecordOutDto? Record { get; set; }
    public string TotalHours { get; set; } = "";
    public TimeSpan TotalTimeSpan { get; set; }
}