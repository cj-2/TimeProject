using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetPaginatedCategoryUseCase
{
    ICustomResult<IPagination<ICategoryOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}