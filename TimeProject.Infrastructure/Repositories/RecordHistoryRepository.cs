using Dapper;
using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.ObjectValues;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class RecordHistoryRepository(CustomDbContext context) : IRecordHistoryRepository
{
    public List<Period> GetPeriodsWithoutSession(int recordId, int userId,
        DateTimeOffset initDate, DateTimeOffset endDate)
    {
        return PeriodQuery(recordId, userId)
            .Where(e =>
                e.SessionId == null
                && e.Start >= initDate
                && e.Start < endDate
                && e.Start < e.End
            )
            .OrderBy(period => period.Start)
            .ToList();
    }

    public List<Minute> GetMinutes(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate)
    {
        return MinuteQuery(recordId, userId)
            .Where(e => e.Date >= initDate && e.Date < endDate)
            .OrderBy(e => e.Date)
            .ToList();
    }

    public List<Session> GetSessions(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate)
    {
        return SessionQuery(recordId, userId)
            .Where(e =>
                e.Periods!.FirstOrDefault()!.Start >= initDate
                && e.Periods!.FirstOrDefault()!.Start < endDate
            )
            .Include(e => e.Periods!.OrderBy(period => period.Start))
            .ToList();
    }

    public List<DateTimeOffset> GetDistinctDates(
        int recordId,
        int userId,
        int limit = 12,
        int offset = 0,
        string utc = "America/Sao_Paulo")
    {
        var sql =
            """
            select date_trunc('day', p.start_period at time zone @utc) as Date
            from periods p
            where p.user_id = @userId and p.record_id = @recordId
            union
            (select date_trunc('day', m.date at time zone @utc) as Date
            from minutes m
            where m.user_id = @userId and m.record_id = @recordId)
            order by Date desc
            limit @limit
            offset @offset;
            """;

        return context.Database.GetDbConnection()
            .Query<DateSearch>(sql, new { userId, recordId, utc, limit, offset })
            .Select(e => e.Date)
            .ToList();
    }

    private IQueryable<Period> PeriodQuery(int recordId, int userId)
    {
        return context.Periods
            .Where(e => e.UserId == userId && e.RecordId == recordId)
            .AsQueryable();
    }

    private IQueryable<Minute> MinuteQuery(int recordId, int userId)
    {
        return context.Minutes
            .Where(e => e.UserId == userId && e.RecordId == recordId)
            .AsQueryable();
    }

    private IQueryable<Session> SessionQuery(int recordId, int userId)
    {
        return context.Sessions
            .Where(e => e.UserId == userId && e.RecordId == recordId)
            .AsQueryable();
    }
}