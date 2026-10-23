using TimeProject.Application.Modules.Records.Dtos;

namespace TimeProject.Application.Modules.Statistics.Dtos;

public class RecordRangeProgress
{
    public RecordOutDto? Record { get; set; }
    public string TotalHours { get; set; } = "";
    public TimeSpan TotalTimeSpan { get; set; }
}