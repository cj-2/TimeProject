using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Categories;

public class DeleteCategoryUseCase(ICategoryRepository repository) : IDeleteCategoryUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var category = repository.FindById(id);

        if (category == null) return result.SetError(CategoryMessageErrors.NotFound);

        if (category.UserId != userId) return result.SetError(GeneralMessageErrors.Unauthorized);

        result.Data = repository.Delete(category);
        return result;
    }
}