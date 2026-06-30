using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Repositories;

public interface ICategoryRepository
{
    IList<Category> Index(int userId, bool onlyWithData);
    IList<Category> Index(IPaginationQuery paginationQuery, int userId);
    int GetTotalItems(IPaginationQuery paginationQuery, int userId);
    Category Create(Category entity);
    Category Update(Category entity);
    bool Delete(Category entity);
    Category? FindById(int id);
    Category? FindById(int id, int userId);
    Category? FindByName(string name, int userId);
}