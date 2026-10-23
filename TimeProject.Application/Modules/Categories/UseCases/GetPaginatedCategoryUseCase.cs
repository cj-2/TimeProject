using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class GetPaginatedCategoryUseCase(ICategoryRepository repository)
    : IGetPaginatedCategoryUseCase
{
    public ICustomResult<Pagination<CategoryOutDto>> Handle(PaginationQuery paginationQuery, int userId)
    {
        var data = repository.Index(paginationQuery, userId).Select(CategoryOutDto.Map);
        var totalItems = repository.GetTotalItems(paginationQuery, userId);

        return new CustomResult<Pagination<CategoryOutDto>>
            { Data = Pagination<CategoryOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}