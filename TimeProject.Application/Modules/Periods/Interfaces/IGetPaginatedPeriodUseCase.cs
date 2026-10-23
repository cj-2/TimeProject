using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Periods.Interfaces;

public interface IGetPaginatedPeriodUseCase
{
    ICustomResult<Pagination<PeriodOutDto>> Handle(int recordId, int userId, PaginationQuery paginationQuery);
}