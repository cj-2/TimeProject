using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodByListUseCase
{
    ICustomResult<IList<IPeriod>> Handle(PeriodListDto dto, int recordId, int userId);
}