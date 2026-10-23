using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface ICreateCategoryUseCase
{
    ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId);
}