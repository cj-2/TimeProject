namespace TimeProject.Infrastructure.ObjectValues.Statistics;

public class RangeStatisticsWithDays
{
    public RangeStatistic Total { get; set; } = new();
    public IList<RangeStatistic> Days { get; set; } = [];
}