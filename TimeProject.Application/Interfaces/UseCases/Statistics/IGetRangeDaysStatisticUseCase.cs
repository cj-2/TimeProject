using TimeProject.Domain.Dtos.Statistics;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Statistics;

public interface IGetRangeDaysStatisticUseCase
{
    public ICustomResult<IRangeStatistic> Handle(int userId, DateTimeOffset? start = null, DateTimeOffset? end = null,
        int? recordId = null, bool skipRangeProgress = false);

    public ICustomResult<IRangeStatisticsWithDays> Handle(int userId, DateTimeOffset start, DateTimeOffset end);
}