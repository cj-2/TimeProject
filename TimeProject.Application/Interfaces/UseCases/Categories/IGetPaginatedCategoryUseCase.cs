using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetPaginatedCategoryUseCase
{
    ICustomResult<Pagination<CategoryOutDto>> Handle(PaginationQuery paginationQuery, int userId);
}