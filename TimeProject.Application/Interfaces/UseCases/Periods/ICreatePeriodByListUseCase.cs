using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodByListUseCase
{
    ICustomResult<IList<Period>> Handle(PeriodListDto dto, int recordId, int userId);
}