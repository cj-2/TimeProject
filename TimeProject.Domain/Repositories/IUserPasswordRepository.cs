using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IUserPasswordRepository
{
    public bool Create(UserPassword entity);
    public bool Update(UserPassword entity);
    public bool Delete(int id);
    public UserPassword? FindByUserId(int userId);
}