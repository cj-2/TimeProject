using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface ISyncRecordResumeUseCase
{
    RecordResume? Handle(int id, bool saveChanges = true);
    RecordResume? Handle(Record record, bool saveChanges = false);
    IEnumerable<RecordResume> Handle(IEnumerable<Record> recordEntities, bool saveChanges = false);
}