using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;

namespace TimeProject.Application.UseCases.Categories;

public class GetPaginatedCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper)
    : IGetPaginatedCategoryUseCase
{
    public ICustomResult<IPagination<ICategoryOutDto>> Handle(IPaginationQuery paginationQuery, int userId)
    {
        var data = mapper.Handle(repository.Index(paginationQuery, userId));
        var totalItems = repository.GetTotalItems(paginationQuery, userId);

        return new CustomResult<IPagination<ICategoryOutDto>>
            { Data = Pagination<ICategoryOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}