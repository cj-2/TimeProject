using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class ConfirmCodeRepository(CustomDbContext db) : IConfirmCodeRepository
{
    public ConfirmCode Create(ConfirmCode entity)
    {
        db.ConfirmCodes.Add(entity);
        db.SaveChanges();
        return entity;
    }

    public ConfirmCode Update(ConfirmCode entity)
    {
        db.ConfirmCodes.Update(entity);
        db.SaveChanges();
        return entity;
    }

    public ConfirmCode? FindByIdAndEmail(string id, string email)
    {
        return db.ConfirmCodes.Include(e => e.User)
            .FirstOrDefault(e => e.CodeId == id && e.User!.Email == email);
    }

    public IList<ConfirmCode> FindByUserId(int userId)
    {
        return db.ConfirmCodes
            .Where(e => e.UserId == userId)
            .ToList<ConfirmCode>();
    }

    public IList<ConfirmCode> FindByUserId(int userId, ConfirmCodeType type)
    {
        return db.ConfirmCodes
            .Where(e => e.UserId == userId && e.Type == type)
            .ToList<ConfirmCode>();
    }

    public IList<ConfirmCode> FindByUserIdThatIsNotExpiredOrUsed(int userId, ConfirmCodeType type)
    {
        var now = DateTime.Now.ToUniversalTime();
        return db.ConfirmCodes
            .Where(e => e.UserId == userId && e.Type == type && now < e.Expiration && e.IsUsed == false)
            .ToList<ConfirmCode>();
    }

    public ConfirmCode? FindById(string id)
    {
        return db.ConfirmCodes.FirstOrDefault(e => e.CodeId == id);
    }
}