using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IStatisticRepository
{
    IList<Period> GetPeriodsByRange(int userId, DateTimeOffset initDate, DateTimeOffset endDate,
        int? recordId = null);

    IList<Session> GetSessionsByRange(int userId, DateTimeOffset initDate, DateTimeOffset endDate,
        int? recordId = null);

    IList<Minute> GetTimeMinutesByRange(int userId, DateTimeOffset initDate, DateTimeOffset endDate,
        int? recordId = null);

    int GetRecordCreatedCount(int userId, DateTimeOffset initDate, DateTimeOffset endDate);
    int GetRecordUpdatedCount(int userId, DateTimeOffset initDate, DateTimeOffset endDate);
}