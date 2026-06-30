using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class UserProviderRepository(CustomDbContext db) : IUserProviderRepository
{
    public UserProvider Create(UserProvider entity)
    {
        db.UserProviders.Add(entity);
        return entity;
    }

    public bool Delete(int id)
    {
        var entity = FindByUserId(id);
        if (entity == null) return true;

        db.UserProviders.Remove(entity);
        return true;
    }

    public UserProvider? FindByUserId(int id)
    {
        return db.UserProviders.FirstOrDefault(i => i.UserId == id);
    }

    public UserProvider? FindByUserProviderId(string provider, string id)
    {
        return db.UserProviders.FirstOrDefault(i => i.Provider == provider && i.ExternalId == id);
    }
}