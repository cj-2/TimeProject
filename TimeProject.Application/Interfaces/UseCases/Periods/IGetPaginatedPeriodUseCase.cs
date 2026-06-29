using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IGetPaginatedPeriodUseCase
{
    ICustomResult<IPagination<PeriodOutDto>> Handle(int recordId, int userId, IPaginationQuery paginationQuery);
}