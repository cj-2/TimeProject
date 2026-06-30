using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class UserAccessLogRepository(CustomDbContext db) : IUserAccessLogRepository
{
    public UserAccessLog Create(UserAccessLog entity)
    {
        entity.AccessedAt = DateTime.Now.ToUniversalTime();
        db.UserAccessLogs.Add((UserAccessLog)entity);
        return entity;
    }

    public UserAccessLog? GetLastAccessByUserId(int id)
    {
        return db.UserAccessLogs
            .Where(e => e.UserId == id)
            .OrderByDescending(e => e.AccessedAt)
            .FirstOrDefault();
    }

    public IList<UserAccessLog> GetLastAccessByUserIdList(IEnumerable<int> idList)
    {
        var list = new List<UserAccessLog>();

        foreach (var id in idList)
        {
            DateTime? maxAccessAt = db.UserAccessLogs.Any(e => e.UserId == id)
                ? db.UserAccessLogs.Where(e => e.UserId == id).Max(e => e.AccessedAt)
                : null;

            var lastAccessByUserId = db.UserAccessLogs
                .FirstOrDefault(e => e.UserId == id && e.AccessedAt == maxAccessAt);

            if (lastAccessByUserId is not null)
                list.Add(lastAccessByUserId);
        }

        return list;
    }
}