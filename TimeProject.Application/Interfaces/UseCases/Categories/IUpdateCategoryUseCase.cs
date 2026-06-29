using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IUpdateCategoryUseCase
{
    ICustomResult<ICategory> Handle(int id, ICategoryDto dto, int userId);
}