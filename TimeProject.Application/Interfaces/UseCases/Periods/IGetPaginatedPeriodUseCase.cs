using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IGetPaginatedPeriodUseCase
{
    ICustomResult<IPagination<PeriodOutDto>> Handle(int recordId, int userId, IPaginationQuery paginationQuery);
}