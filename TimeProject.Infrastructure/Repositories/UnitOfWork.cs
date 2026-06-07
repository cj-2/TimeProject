using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Infrastructure.Repositories;

public class UnitOfWork(
    CustomDbContext db,
    IUserRepository userRepository,
    IUserPasswordRepository userPasswordRepository,
    IUserProviderRepository userProviderRepository,
    IUserAccessLogRepository userAccessLogRepository,
    IConfirmCodeRepository confirmCodeRepository,
    ICategoryRepository categoryRepository,
    ISessionRepository sessionRepository,
    IRecordRepository recordRepository,
    IRecordResumeRepository recordResumeRepository,
    IRecordHistoryRepository recordHistoryRepository,
    IPeriodRepository periodRepository,
    IMinuteRepository minuteRepository,
    IStatisticRepository statisticRepository)
    : IUnitOfWork, IDisposable, IAsyncDisposable
{
    public CustomDbContext Context { get; } = db;
    
    public IUserRepository UserRepository { get; } = userRepository;
    public IUserPasswordRepository UserPasswordRepository { get; } = userPasswordRepository;
    public IUserProviderRepository UserProviderRepository { get; } = userProviderRepository;
    public IUserAccessLogRepository UserAccessLogRepository { get; } = userAccessLogRepository;
    public IConfirmCodeRepository ConfirmCodeRepository { get; } = confirmCodeRepository;
    public ICategoryRepository CategoryRepository { get; } = categoryRepository;
    public ISessionRepository SessionRepository { get; } = sessionRepository;
    public IRecordRepository RecordRepository { get; } = recordRepository;
    public IRecordResumeRepository RecordResumeRepository { get; } = recordResumeRepository;
    public IRecordHistoryRepository RecordHistoryRepository { get; } = recordHistoryRepository;
    public IPeriodRepository PeriodRepository { get; } = periodRepository;
    public IMinuteRepository MinuteRepository { get; } = minuteRepository;
    public IStatisticRepository StatisticRepository { get; } = statisticRepository;
    
    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}