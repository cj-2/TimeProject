using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Categories.Interfaces;

public interface IDeleteCategoryUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}