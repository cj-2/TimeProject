using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Modules.Periods.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Periods.UseCases;

public class GetPaginatedPeriodUseCase(IPeriodRepository repository, IPeriodMapDataUtil mapDataUtil)
    : IGetPaginatedPeriodUseCase
{
    public ICustomResult<Pagination<PeriodOutDto>> Handle(int recordId, int userId,
        PaginationQuery paginationQuery)
    {
        var totalItems = repository.GetTotalItems(recordId, paginationQuery, userId);
        var data = mapDataUtil.Handle(repository.Index(recordId, userId, paginationQuery).ToList());

        return new CustomResult<Pagination<PeriodOutDto>>
        {
            Data = Pagination<PeriodOutDto>.Handle(
                data,
                paginationQuery,
                totalItems
            )
        };
    }
}