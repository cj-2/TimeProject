using TimeProject.Application.Dtos.Statistics;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Statistics;

public interface IGetRangeDaysStatisticUseCase
{
    public ICustomResult<RangeStatistic> Handle(int userId, DateTimeOffset? start = null, DateTimeOffset? end = null,
        int? recordId = null, bool skipRangeProgress = false);

    public ICustomResult<RangeStatisticsWithDays> Handle(int userId, DateTimeOffset start, DateTimeOffset end);
}