using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IGetPaginatedPeriodUseCase
{
    ICustomResult<Pagination<PeriodOutDto>> Handle(int recordId, int userId, PaginationQuery paginationQuery);
}