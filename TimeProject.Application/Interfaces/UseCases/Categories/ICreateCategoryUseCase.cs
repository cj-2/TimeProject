using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface ICreateCategoryUseCase
{
    ICustomResult<ICategoryOutDto> Handle(ICategoryDto dto, int userId);
}