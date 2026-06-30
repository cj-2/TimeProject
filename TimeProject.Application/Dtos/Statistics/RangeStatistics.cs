using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatistics
{
    public RangeStatisticOutDto StatisticOutDto { get; set; } = new();
    public IList<Period> Periods { get; set; } = [];
    public IList<Minute> Minutes { get; set; } = [];
    public IList<Session> Sessions { get; set; } = [];
}