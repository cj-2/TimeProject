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
        var user = userRepository.FindById(userId);
        if (user == null)
        {
            return new CustomResult<Pagination<RecordHistoryDayOutDto>>().SetError(UserMessageErrors.NotFound);
        }

        // É passado um int referente ao UTC entre -12 e 13, para que consigamos saber as datas do UTC do usuário.
        var distinctDates = repository.GetDistinctDates(recordId, userId, -3);

        var dates = distinctDates
            .Skip((paginationQuery.Page - 1) * paginationQuery.PerPage)
            .Take(paginationQuery.PerPage);

        var historyDays = new List<RecordHistoryDayDto>();

        foreach (var dateItem in dates)
        {
            // initDate pega a data que já subtraimos a diferênça de utc e somamos novamente para fazer a busca correnta.
            var initDate = dateItem.AddHours(-3);
            var endDate = initDate.AddDays(1);

            var tpList = repository.GetPeriodsWithoutSession(recordId, userId, initDate, endDate);
            var tsList = repository.GetSessions(recordId, userId, initDate, endDate);
            var tmList = repository.GetMinutes(recordId, userId, initDate, endDate);

            if (tpList.Count == 0 && tsList.Count == 0 && tmList.Count == 0) continue;

            historyDays.Add(new RecordHistoryDayDto
            {
                Date = initDate,
                InitDate = initDate,
                EndDate = endDate,
                Periods = tpList,
                Minutes = tmList,
                Sessions = tsList
            });
        }

        return new CustomResult<Pagination<RecordHistoryDayOutDto>>
        {
            Data = Pagination<RecordHistoryDayOutDto>
                .Handle(mapDataUtil.Handle(historyDays), paginationQuery, distinctDates.Count)
        };
    }
}