using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IGetAllCategoryUseCase
{
    ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData);
}