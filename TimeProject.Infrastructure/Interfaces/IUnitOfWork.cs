using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserPasswordRepository UserPasswordRepository { get; }
    IUserProviderRepository UserProviderRepository { get; }
    IUserAccessLogRepository UserAccessLogRepository { get; }
    IConfirmCodeRepository IConfirmCodeRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ISessionRepository SessionRepository { get; }
    IRecordRepository RecordRepository { get; }
    IRecordResumeRepository RecordResumeRepository { get; }
    IRecordHistoryRepository RecordHistoryRepository { get; }
    IPeriodRepository PeriodRepository { get; }
    IMinuteRepository MinuteRepository { get; }
    IStatisticRepository StatisticRepository { get; }

    CustomDbContext Context { get; }
    
    void SaveChanges();
    Task SaveChangesAsync();
}