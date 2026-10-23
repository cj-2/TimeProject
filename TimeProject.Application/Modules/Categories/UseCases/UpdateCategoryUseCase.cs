using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class UpdateCategoryUseCase(ICategoryRepository repository) : IUpdateCategoryUseCase
{
    public ICustomResult<Category> Handle(int id, CategoryDto dto, int userId)
    {
        var result = new CustomResult<Category>();
        var category = repository.FindById(id);

        if (category == null) return result.SetError(CategoryErrors.NotFound);

        if (category.UserId != userId) return result.SetError(GeneralErrors.Unauthorized);

        if (repository.FindByName(dto.Name, userId) != null)
            return result.SetError(CategoryErrors.AlreadyExists);

        category.Name = dto.Name;
        result.Data = repository.Update(category);
        return result;
    }
}