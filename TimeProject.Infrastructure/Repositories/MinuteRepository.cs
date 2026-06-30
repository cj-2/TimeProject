using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class MinuteRepository(CustomDbContext db) : IMinuteRepository
{
    public Minute Create(Minute entity)
    {
        db.Minutes.Add(entity);
        db.SaveChanges();
        return entity;
    }

    public IList<Minute> CreateByList(IList<Minute> entities)
    {
        db.Minutes.AddRange(entities);
        db.SaveChanges();
        return entities;
    }

    public Minute? FindById(int id, int userId)
    {
        return db.Minutes.FirstOrDefault(e => e.MinuteId == id && e.UserId == userId);
    }

    public bool Delete(Minute entity)
    {
        db.Minutes.Remove(entity);
        db.SaveChanges();
        return true;
    }
}