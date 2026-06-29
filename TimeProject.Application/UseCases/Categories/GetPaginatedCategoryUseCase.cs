using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Dtos.General;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;

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