using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Categories;

public interface IDeleteCategoryUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}