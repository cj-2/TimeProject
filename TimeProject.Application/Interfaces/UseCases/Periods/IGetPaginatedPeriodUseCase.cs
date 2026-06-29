using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IGetPaginatedPeriodUseCase
{
    ICustomResult<IPagination<IPeriodOutDto>> Handle(int recordId, int userId, IPaginationQuery paginationQuery);
}