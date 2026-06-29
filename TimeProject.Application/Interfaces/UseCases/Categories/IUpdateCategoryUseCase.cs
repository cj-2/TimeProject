using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IUpdateCategoryUseCase
{
    ICustomResult<ICategory> Handle(int id, CategoryDto dto, int userId);
}