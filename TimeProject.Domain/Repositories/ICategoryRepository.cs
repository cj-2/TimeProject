using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Domain.Repositories;

public interface ICategoryRepository
{
    List<Category> Index(int userId, bool onlyWithData);
    IList<Category> Index(PaginationQuery paginationQuery, int userId);
    int GetTotalItems(PaginationQuery paginationQuery, int userId);
    Category Create(Category entity);
    Category Update(Category entity);
    bool Delete(Category entity);
    Category? FindById(int id);
    Category? FindById(int id, int userId);
    Category? FindByName(string name, int userId);
}