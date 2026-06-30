using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IRecordResumeRepository
{
    RecordResume? CreateOrUpdate(int recordId, bool saveChanges = true);
    RecordResume? CreateOrUpdate(Record record, bool saveChanges = false);
    IEnumerable<RecordResume> CreateOrUpdateList(IEnumerable<Record> recordEntities, bool saveChanges = false);
}