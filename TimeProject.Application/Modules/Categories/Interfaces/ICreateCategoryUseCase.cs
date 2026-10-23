using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface ICreateCategoryUseCase
{
    ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId);
}