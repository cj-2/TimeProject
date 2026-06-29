using TimeProject.Application.Dtos.Records;

namespace TimeProject.Application.Dtos.Statistics;

public class RecordRangeProgress
{
    public RecordOutDto? Record { get; set; }
    public string TotalHours { get; set; } = "";
    public TimeSpan TotalTimeSpan { get; set; }
}