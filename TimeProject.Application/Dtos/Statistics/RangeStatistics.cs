using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatistics
{
    public RangeStatisticOutDto StatisticOutDto { get; set; } = new RangeStatisticOutDto();
    public IList<IPeriod> Periods { get; set; } = [];
    public IList<IMinute> Minutes { get; set; } = [];
    public IList<ISession> Sessions { get; set; } = [];
}