using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetPaginatedCategoryUseCase
{
    ICustomResult<IPagination<CategoryOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}