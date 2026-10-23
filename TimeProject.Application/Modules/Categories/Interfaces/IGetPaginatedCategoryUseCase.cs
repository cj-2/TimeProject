using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface IGetPaginatedCategoryUseCase
{
    ICustomResult<Pagination<CategoryOutDto>> Handle(PaginationQuery paginationQuery, int userId);
}