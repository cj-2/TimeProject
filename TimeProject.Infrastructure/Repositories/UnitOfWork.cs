using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Infrastructure.Repositories;

public class UnitOfWork(
    CustomDbContext context,
    IUserRepository user,
    IUserPasswordRepository userPassword,
    IUserProviderRepository userProvider,
    IUserAccessLogRepository userAccessLog,
    IConfirmCodeRepository confirmCode,
    ICategoryRepository category,
    ISessionRepository session,
    IRecordRepository record,
    IRecordResumeRepository recordResume,
    IRecordHistoryRepository recordHistory,
    IPeriodRepository period,
    IMinuteRepository minute,
    IStatisticRepository statistic)
    : IUnitOfWork, IDisposable, IAsyncDisposable
{
    public IUserRepository User { get; } = user;
    public IUserPasswordRepository UserPassword { get; } = userPassword;
    public IUserProviderRepository UserProvider { get; } = userProvider;
    public IUserAccessLogRepository UserAccessLog { get; } = userAccessLog;
    public IConfirmCodeRepository ConfirmCode { get; } = confirmCode;
    public ICategoryRepository Category { get; } = category;
    public ISessionRepository Session { get; } = session;
    public IRecordRepository Record { get; } = record;
    public IRecordResumeRepository RecordResume { get; } = recordResume;
    public IRecordHistoryRepository RecordHistory { get; } = recordHistory;
    public IPeriodRepository Period { get; } = period;
    public IMinuteRepository Minute { get; } = minute;
    public IStatisticRepository Statistic { get; } = statistic;

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void BeginTransaction()
    {
        context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        context.Database.RollbackTransaction();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}