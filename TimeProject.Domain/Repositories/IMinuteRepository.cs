using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IMinuteRepository
{
    Minute Create(Minute entity);
    IList<Minute> CreateByList(IList<Minute> entities);
    Minute? FindById(int id, int userId);
    bool Delete(Minute entity);
}