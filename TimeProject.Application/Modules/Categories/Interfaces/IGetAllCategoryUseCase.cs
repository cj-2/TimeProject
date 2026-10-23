using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface IGetAllCategoryUseCase
{
    ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData);
}