using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodByListUseCase
{
    ICustomResult<IList<IPeriod>> Handle(IPeriodListDto dto, int recordId, int userId);
}