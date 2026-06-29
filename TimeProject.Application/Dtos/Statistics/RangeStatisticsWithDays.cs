namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatisticsWithDays
{
    public RangeStatistic Total { get; set; } = new();
    public IList<RangeStatistic> Days { get; set; } = [];
}