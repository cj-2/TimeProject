using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatistics
{
    public RangeStatistic Statistic { get; set; } = new();
    public List<Period> Periods { get; set; } = [];
    public List<Minute> Minutes { get; set; } = [];
    public List<Session> Sessions { get; set; } = [];
}