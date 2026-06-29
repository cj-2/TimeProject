using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Records;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Application.UseCases.Records;

public class GetRecordHistoryUseCase(
    IRecordHistoryRepository repository,
    IUserRepository userRepository,
    IRecordMapDataUtil mapDataUtil) : IGetRecordHistoryUseCase
{
    public ICustomResult<IPagination<IRecordHistoryDayOutDto>> Handle(
        int recordId,
        int userId,
        IPaginationQuery paginationQuery
    )
    {
        var user = userRepository.FindById(userId);
        if (user == null)
        {
            return new CustomResult<IPagination<IRecordHistoryDayOutDto>>().SetError(UserMessageErrors.NotFound);
        }

        // É passado um int referente ao UTC entre -12 e 13, para que consigamos saber as datas do UTC do usuário.
        var distinctDates = repository.GetDistinctDates(recordId, userId, -3);

        var dates = distinctDates
            .Skip((paginationQuery.Page - 1) * paginationQuery.PerPage)
            .Take(paginationQuery.PerPage);

        var historyDays = new List<RecordHistoryDay>();

        foreach (var dateItem in dates)
        {
            // initDate pega a data que já subtraimos a diferênça de utc e somamos novamente para fazer a busca correnta.
            var initDate = dateItem.AddHours(-3);
            var endDate = initDate.AddDays(1);

            var tpList = repository.GetPeriodsWithoutSession(recordId, userId, initDate, endDate);
            var tsList = repository.GetSessions(recordId, userId, initDate, endDate);
            var tmList = repository.GetMinutes(recordId, userId, initDate, endDate);

            if (tpList.Count == 0 && tsList.Count == 0 && tmList.Count == 0) continue;

            historyDays.Add(new RecordHistoryDay
            {
                Date = initDate,
                InitDate = initDate,
                EndDate = endDate,
                Periods = tpList,
                Minutes = tmList,
                Sessions = tsList
            });
        }

        return new CustomResult<IPagination<IRecordHistoryDayOutDto>>
        {
            Data = Pagination<IRecordHistoryDayOutDto>
                .Handle(mapDataUtil.Handle(historyDays), paginationQuery, distinctDates.Count)
        };
    }
}