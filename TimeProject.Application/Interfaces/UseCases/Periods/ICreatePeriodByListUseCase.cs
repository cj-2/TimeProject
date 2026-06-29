using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodByListUseCase
{
    ICustomResult<IList<IPeriod>> Handle(PeriodListDto dto, int recordId, int userId);
}