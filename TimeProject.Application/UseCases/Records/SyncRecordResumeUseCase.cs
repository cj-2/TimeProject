using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Records;

public class SyncRecordResumeUseCase(IRecordResumeRepository repository) : ISyncRecordResumeUseCase
{
    public IRecordResume? Handle(int id, bool saveChanges = true)
    {
        return repository.CreateOrUpdate(id, saveChanges);
    }

    public IRecordResume? Handle(IRecord record, bool saveChanges = false)
    {
        return repository.CreateOrUpdate(record, saveChanges);
    }

    public IEnumerable<IRecordResume> Handle(IEnumerable<IRecord> recordEntities,
        bool saveChanges = false)
    {
        return repository.CreateOrUpdateList(recordEntities, saveChanges);
    }
}