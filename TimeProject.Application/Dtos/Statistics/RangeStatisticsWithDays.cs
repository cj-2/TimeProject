namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatisticsWithDays
{
    public RangeStatistic Total { get; set; } = new();
    public List<RangeStatistic> Days { get; set; } = [];
}