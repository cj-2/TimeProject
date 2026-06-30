using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class StatisticRepository(CustomDbContext db) : IStatisticRepository
{
    public IList<Period> GetPeriodsByRange(int userId,
        DateTimeOffset initDate,
        DateTimeOffset endDate,
        int? recordId = null)
    {
        var query = db.Periods.AsQueryable();

        if (recordId != null) query = query.Where(i => i.RecordId == recordId);

        return query.Where(e => (
                (e.Start >= initDate && e.Start < endDate) || (e.End > initDate && e.End <= endDate)
            ) && userId == e.UserId)
            .ToList();
    }

    public IList<Session> GetSessionsByRange(int userId,
        DateTimeOffset initDate,
        DateTimeOffset endDate,
        int? recordId = null)
    {
        var query = db.Sessions.AsQueryable();

        if (recordId != null) query = query.Where(i => i.RecordId == recordId);

        return query
            .Where(p =>
                (
                    (
                        p.Periods!.Count(e => e.Start >= initDate) > 0
                        &&
                        p.Periods!.Count(e => e.Start < endDate) > 0
                    )
                    ||
                    (
                        p.Periods!.Count(e => e.End > initDate) > 0
                        &&
                        p.Periods!.Count(e => e.End <= endDate) > 0
                    )
                )
                && userId == p.UserId)
            .Include(p => p.Periods!.OrderBy(q => q.Start))
            .ToList();
    }

    public IList<Minute> GetTimeMinutesByRange(int userId, DateTimeOffset initDate, DateTimeOffset endDate,
        int? recordId = null)
    {
        var query = db.Minutes.AsQueryable();

        if (recordId != null) query = query.Where(i => i.RecordId == recordId);

        return query.Where(tm =>
                tm.UserId == userId
                && tm.Date >= initDate
                && tm.Date < endDate
            )
            .OrderBy(tm => tm.Date)
            .ToList();
    }

    public int GetRecordCreatedCount(int userId, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        return db.Records.Count(e => e.CreatedAt >= initDate && e.CreatedAt < endDate && userId == e.UserId);
    }

    public int GetRecordUpdatedCount(int userId, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        return db.Records
            .Count(e => e.UpdatedAt >= initDate
                        && e.UpdatedAt < endDate
                        && e.UpdatedAt != e.CreatedAt
                        && userId == e.UserId);
    }
}