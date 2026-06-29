using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatisticsData
{
    public RangeStatistic Statistic { get; set; } = new RangeStatistic();
    public IList<IPeriod> Periods { get; set; } = [];
    public IList<IMinute> Minutes { get; set; } = [];
    public IList<ISession> Sessions { get; set; } = [];
}