using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISyncAllRecordResumeUseCase
{
    ICustomResult<IEnumerable<RecordResume>> Handle();
}