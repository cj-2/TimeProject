using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Periods;

public class CreatePeriodByListUseCase(
    IPeriodRepository repository,
    ISessionRepository sessionRepository,
    ISyncRecordResumeUseCase syncRecordResumeUseCase,
    IPeriodValidateUtil periodValidateUtil
) : ICreatePeriodByListUseCase
{
    public ICustomResult<IList<IPeriod>> Handle(PeriodListDto dto, int recordId, int userId)
    {
        var result = new CustomResult<IList<IPeriod>>();
        List<Period> list = [];

        foreach (var period in dto.Periods)
        {
            periodValidateUtil.ValidateStartAndEnd(period.Start, period.End, result);
            if (result.HasError)
                break;

            if (periodValidateUtil.HasMinSize(period))
                list.Add(new Period
                {
                    UserId = userId,
                    RecordId = recordId,
                    Start = period.Start,
                    End = period.End
                });
        }

        if (result.HasError) return result;
        if (list.Count == 0) return result.SetData([]);


        var session = sessionRepository
            .Create(new Session
                {
                    RecordId = recordId,
                    UserId = userId,
                    Type = dto.Type ?? SessionType.Default,
                    From = dto.From
                }
            );


        list.ForEach(i => { i.SessionId = session.SessionId; });

        var data = repository.CreateByList(list.ToList<IPeriod>());
        syncRecordResumeUseCase.Handle(recordId);

        return result.SetData(data);
    }
}