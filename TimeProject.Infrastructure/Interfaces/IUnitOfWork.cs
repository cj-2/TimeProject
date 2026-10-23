using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    IUserPasswordRepository UserPassword { get; }
    IUserProviderRepository UserProvider { get; }
    IUserAccessLogRepository UserAccessLog { get; }
    IConfirmCodeRepository ConfirmCode { get; }
    ICategoryRepository Category { get; }
    ISessionRepository Session { get; }
    IRecordRepository Record { get; }
    IRecordResumeRepository RecordResume { get; }
    IRecordHistoryRepository RecordHistory { get; }
    IPeriodRepository Period { get; }
    IMinuteRepository Minute { get; }
    IStatisticRepository Statistic { get; }

    void SaveChanges();
    Task SaveChangesAsync();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}