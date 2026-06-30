using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Records;

public class SyncRecordResumeUseCase(IRecordResumeRepository repository) : ISyncRecordResumeUseCase
{
    public RecordResume? Handle(int id, bool saveChanges = true)
    {
        return repository.CreateOrUpdate(id, saveChanges);
    }

    public RecordResume? Handle(Record record, bool saveChanges = false)
    {
        return repository.CreateOrUpdate(record, saveChanges);
    }

    public IEnumerable<RecordResume> Handle(IEnumerable<Record> recordEntities,
        bool saveChanges = false)
    {
        return repository.CreateOrUpdateList(recordEntities, saveChanges);
    }
}