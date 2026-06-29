using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.UseCases.Categories;

public class GetPaginatedCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper)
    : IGetPaginatedCategoryUseCase
{
    public ICustomResult<IPagination<CategoryOutDto>> Handle(IPaginationQuery paginationQuery, int userId)
    {
        var data = mapper.Handle(repository.Index(paginationQuery, userId));
        var totalItems = repository.GetTotalItems(paginationQuery, userId);

        return new CustomResult<IPagination<CategoryOutDto>>
            { Data = Pagination<CategoryOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}