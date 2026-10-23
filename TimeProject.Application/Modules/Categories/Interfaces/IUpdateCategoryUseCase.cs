using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface IUpdateCategoryUseCase
{
    ICustomResult<Category> Handle(int id, CategoryDto dto, int userId);
}