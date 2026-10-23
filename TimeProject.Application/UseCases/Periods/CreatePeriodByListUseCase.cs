using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Shared;
using TimeProject.Domain;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Periods;

public class CreatePeriodByListUseCase(IPeriodRepository repository, ISessionRepository sessionRepository)
    : ICreatePeriodByListUseCase
{
    public ICustomResult<IList<Period>> Handle(PeriodListDto dto, int recordId, int userId)
    {
        var result = new CustomResult<IList<Period>>();
        List<Period> list;

        try
        {
            list = dto.Periods
                .OrderBy(p => p.Start)
                .Select(p => new Period(p.Start, p.End))
                .ToList();
        }
        catch (DomainException ex)
        {
            return result.SetError(ex.Message);
        }

        var session = sessionRepository
            .Create(new Session
                {
                    RecordId = recordId,
                    UserId = userId,
                    Type = dto.Type ?? SessionType.Default,
                    From = dto.From,
                    Date = list.First().Start
                }
            );

        list.ForEach(period => { period.SessionId = session.SessionId; });

        var data = repository.CreateByList(list);
        return result.SetData(data);
    }
}