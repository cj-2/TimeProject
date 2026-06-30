using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IUpdateCategoryUseCase
{
    ICustomResult<Category> Handle(int id, CategoryDto dto, int userId);
}