using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Periods;

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