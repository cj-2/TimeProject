using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories.ObjectValues;

namespace TimeProject.Domain.Repositories;

public interface IRecordHistoryRepository
{
    List<DateTimeOffset> GetDistinctDates(
        int recordId, 
        int userId, 
        int limit = 12,
        int offset = 0, 
        string utc = "America/Sao_Paulo");

    List<Period> GetPeriodsWithoutSession(int recordId, int userId,
        DateTimeOffset initDate, DateTimeOffset endDate);

    List<Minute> GetMinutes(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate);

    List<Session> GetSessions(int recordId, int userId, DateTimeOffset initDate,
        DateTimeOffset endDate);
}