using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface ICreateCategoryUseCase
{
    ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId);
}