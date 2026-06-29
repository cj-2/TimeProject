using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetPaginatedCategoryUseCase
{
    ICustomResult<IPagination<CategoryOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}