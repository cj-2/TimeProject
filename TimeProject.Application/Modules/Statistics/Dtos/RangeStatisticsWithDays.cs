using TimeProject.Application.Modules.Statistics.Dtos;

namespace TimeProject.Application.Modules.Statistics.Dtos;

public class RangeStatisticsWithDays
{
    public RangeStatistic Total { get; set; } = new();
    public List<RangeStatistic> Days { get; set; } = [];
}