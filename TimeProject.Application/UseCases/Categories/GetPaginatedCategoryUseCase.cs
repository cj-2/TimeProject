using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.UseCases.Categories;

public class GetPaginatedCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper)
    : IGetPaginatedCategoryUseCase
{
    public ICustomResult<Pagination<CategoryOutDto>> Handle(PaginationQuery paginationQuery, int userId)
    {
        var data = mapper.Handle(repository.Index(paginationQuery, userId));
        var totalItems = repository.GetTotalItems(paginationQuery, userId);

        return new CustomResult<Pagination<CategoryOutDto>>
            { Data = Pagination<CategoryOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}