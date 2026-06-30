using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IRecordHistoryRepository
{
    IList<DateTime> GetDistinctDates(int recordId, int userId, int addHours = 0);

    IList<Period> GetPeriodsWithoutSession(int recordId, int userId,
        DateTime initDate, DateTime endDate);

    IList<Minute> GetMinutes(int recordId, int userId, DateTime initDate,
        DateTime endDate);

    IList<Session> GetSessions(int recordId, int userId, DateTime initDate,
        DateTime endDate);
}