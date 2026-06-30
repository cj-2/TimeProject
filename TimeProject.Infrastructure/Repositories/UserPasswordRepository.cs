using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class UserPasswordRepository(CustomDbContext db) : IUserPasswordRepository
{
    public bool Create(UserPassword entity)
    {
        db.UserPasswords.Add(entity);
        return true;
    }

    public bool Update(UserPassword entity)
    {
        db.UserPasswords.Update(entity);
        return true;
    }

    public bool Delete(int id)
    {
        var entity = FindByUserId(id);
        if (entity == null) return true;

        db.UserPasswords.Remove(entity);
        return true;
    }

    public UserPassword? FindByUserId(int userId)
    {
        return db.UserPasswords.FirstOrDefault(i => i.UserId == userId);
    }
}