using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Categories;

public class UpdateCategoryUseCase(ICategoryRepository repository) : IUpdateCategoryUseCase
{
    public ICustomResult<ICategory> Handle(int id, ICategoryDto dto, int userId)
    {
        var result = new CustomResult<ICategory>();
        var category = repository.FindById(id);

        if (category == null) return result.SetError(CategoryMessageErrors.NotFound);

        if (category.UserId != userId) return result.SetError(GeneralMessageErrors.Unauthorized);

        if (repository.FindByName(dto.Name, userId) != null)
            return result.SetError(CategoryMessageErrors.AlreadyExists);

        category.Name = dto.Name;
        result.Data = repository.Update(category);
        return result;
    }
}