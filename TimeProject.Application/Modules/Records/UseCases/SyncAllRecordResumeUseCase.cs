using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Application.Modules.Records.UseCases;

public class SyncAllRecordResumeUseCase(
    CustomDbContext db,
    ISyncRecordResumeUseCase syncRecordResumeUseCase
)
    : ISyncAllRecordResumeUseCase
{
    public ICustomResult<IEnumerable<RecordResume>> Handle()
    {
        var list = db.Records.ToList();
        return new CustomResult<IEnumerable<RecordResume>> { Data = syncRecordResumeUseCase.Handle(list, true) };
    }
}