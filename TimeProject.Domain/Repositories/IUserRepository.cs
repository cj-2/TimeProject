using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Domain.Repositories;

public interface IUserRepository
{
    IList<User> Index(PaginationQuery paginationQuery);
    int GetTotalItems(PaginationQuery paginationQuery);
    User Create(User entity);
    User Update(User entity);
    bool Delete(int id);
    User? FindById(int id);
    User? FindByEmail(string email);
    bool EmailIsAvailable(string email);
}