using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class SessionRepository(CustomDbContext db) : ISessionRepository
{
    public Session Create(Session entity)
    {
        db.Sessions.Add(entity);
        db.SaveChanges();
        return entity;
    }

    public Session? FindById(int id, int userId)
    {
        return db.Sessions
            .Include(e => e.Periods)
            .FirstOrDefault(e => e.SessionId == id && e.UserId == userId);
    }

    public bool Delete(Session entity)
    {
        db.Sessions.Remove(entity);
        db.SaveChanges();
        return true;
    }
}