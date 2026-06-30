using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Repositories;

public interface IUserRepository
{
    IList<User> Index(IPaginationQuery paginationQuery);
    int GetTotalItems(IPaginationQuery paginationQuery);
    User Create(User entity);
    User Update(User entity);
    bool Delete(int id);
    User? FindById(int id);
    User? FindByEmail(string email);
    bool EmailIsAvailable(string email);
}