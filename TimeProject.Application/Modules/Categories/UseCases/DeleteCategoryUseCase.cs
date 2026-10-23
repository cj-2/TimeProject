using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class DeleteCategoryUseCase(ICategoryRepository repository) : IDeleteCategoryUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var category = repository.FindById(id);

        if (category == null) return result.SetError(CategoryErrors.NotFound);

        if (category.UserId != userId) return result.SetError(GeneralErrors.Unauthorized);

        result.Data = repository.Delete(category);
        return result;
    }
}