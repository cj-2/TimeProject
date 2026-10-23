using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface ISyncAllRecordResumeUseCase
{
    ICustomResult<IEnumerable<RecordResume>> Handle();
}