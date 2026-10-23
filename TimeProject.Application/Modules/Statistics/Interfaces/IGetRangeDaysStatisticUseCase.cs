using TimeProject.Application.Modules.Statistics.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Statistics.Interfaces;

public interface IGetRangeDaysStatisticUseCase
{
    public ICustomResult<RangeStatistic> Handle(int userId, DateTimeOffset? start = null, DateTimeOffset? end = null,
        int? recordId = null, bool skipRangeProgress = false);

    public ICustomResult<RangeStatisticsWithDays> Handle(int userId, DateTimeOffset start, DateTimeOffset end);
}