using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IRecordHistoryRepository
{
    List<DateTimeOffset> GetDistinctDates(int recordId, int userId, string utc = "America/Sao_Paulo");

    List<Period> GetPeriodsWithoutSession(int recordId, int userId,
        DateTimeOffset initDate, DateTimeOffset endDate);

    List<Minute> GetMinutes(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate);

    List<Session> GetSessions(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate);
}