using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Records;

public class GetRecordHistoryUseCase(
    IRecordHistoryRepository repository,
    IUserRepository userRepository,
    IRecordMapDataUtil mapDataUtil) : IGetRecordHistoryUseCase
{
    public ICustomResult<Pagination<RecordHistoryDayOutDto>> Handle(int recordId,
        int userId,
        PaginationQuery paginationQuery)
    {
        var distinctDates = repository
            .GetDistinctDates(recordId, userId, paginationQuery.PerPage, (paginationQuery.Page - 1) * paginationQuery.PerPage)
            .ToList();

        var historyDays = new List<RecordHistoryDayDto>();

        foreach (var initDate in distinctDates)
        {
            var endDate = initDate.AddDays(1);

            var periods = repository.GetPeriodsWithoutSession(recordId, userId, initDate, endDate);
            var sessions = repository.GetSessions(recordId, userId, initDate, endDate);
            var minutes = repository.GetMinutes(recordId, userId, initDate, endDate);

            if (periods.Count == 0 && sessions.Count == 0 && minutes.Count == 0) continue;

            historyDays.Add(new RecordHistoryDayDto
            {
                Date = initDate,
                InitDate = initDate,
                EndDate = endDate,
                Periods = periods,
                Minutes = minutes,
                Sessions = sessions
            });
        }

        return new CustomResult<Pagination<RecordHistoryDayOutDto>>
        {
            Data = Pagination<RecordHistoryDayOutDto>
                .Handle(mapDataUtil.Handle(historyDays), paginationQuery, historyDays.Count)
        };
    }
}