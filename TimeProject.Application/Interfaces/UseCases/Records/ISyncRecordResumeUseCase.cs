using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISyncRecordResumeUseCase
{
    IRecordResume? Handle(int id, bool saveChanges = true);
    IRecordResume? Handle(IRecord record, bool saveChanges = false);
    IEnumerable<IRecordResume> Handle(IEnumerable<IRecord> recordEntities, bool saveChanges = false);
}