using Dapper;
using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;
using TimeProject.Infrastructure.Repositories.ObjectValues;

namespace TimeProject.Infrastructure.Repositories;

public class RecordHistoryRepository(CustomDbContext context) : IRecordHistoryRepository
{
    public List<DateTimeOffset> GetDistinctDates(int recordId, int userId, string utc = "America/Sao_Paulo")
    {
        var dates = GetDatesFromPeriods(recordId, userId, utc);
        var datesFromMinutes = GetDatesFromMinutes(recordId, userId, utc);
        dates.AddRange(datesFromMinutes);
        return dates.Distinct().OrderByDescending(e => e).ToList();
    }

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

    private List<DateTimeOffset> GetDatesFromPeriods(int recordId, int userId, string utc)
    {
        var sql =
            """
            select distinct (date_trunc('day', start_period at time zone @utc)) as Date
            from periods
            where user_id = @userId and record_id = @recordId
            order by Date desc;
            """;

        return context.Database.GetDbConnection()
            .Query<DateSearch>(sql, new { userId, recordId, utc })
            .Select(e => e.Date)
            .ToList();
    }

    private List<DateTimeOffset> GetDatesFromMinutes(int recordId, int userId, string utc)
    {
        var sql =
            """
            select distinct (date_trunc('day', date at time zone @utc)) as Date
            from minutes
            where user_id = @userId and record_id = @recordId
            order by Date desc;
            """;

        return context.Database.GetDbConnection()
            .Query<DateSearch>(sql, new { userId, recordId, utc })
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