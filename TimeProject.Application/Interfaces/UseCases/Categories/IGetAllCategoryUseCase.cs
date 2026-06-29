using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetAllCategoryUseCase
{
    ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData);
}