using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface ICreateCategoryUseCase
{
    ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId);
}