using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Periods.Interfaces;

public interface ICreatePeriodByListUseCase
{
    ICustomResult<IList<Period>> Handle(PeriodListDto dto, int recordId, int userId);
}