using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISyncAllRecordResumeUseCase
{
    ICustomResult<IEnumerable<IRecordResume>> Handle();
}