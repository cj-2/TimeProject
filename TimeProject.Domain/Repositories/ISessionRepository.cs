using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface ISessionRepository
{
    Session Create(Session entity);
    Session? FindById(int id, int userId);
    bool Delete(Session entity);
}