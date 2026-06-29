using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetAllCategoryUseCase
{
    ICustomResult<IList<ICategoryOutDto>> Handle(int userId, bool onlyWithData);
}