using TimeProject.Application.Dtos.General;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Periods;

public class GetPaginatedPeriodUseCase(IPeriodRepository repository, IPeriodMapDataUtil mapDataUtil)
    : IGetPaginatedPeriodUseCase
{
    public ICustomResult<IPagination<PeriodOutDto>> Handle(int recordId, int userId,
        IPaginationQuery paginationQuery)
    {
        var totalItems = repository.GetTotalItems(recordId, paginationQuery, userId);
        var data = mapDataUtil.Handle(repository.Index(recordId, userId, paginationQuery).OfType<Period>().ToList());

        return new CustomResult<IPagination<PeriodOutDto>>
        {
            Data = Pagination<PeriodOutDto>.Handle(
                data,
                paginationQuery,
                totalItems
            )
        };
    }
}