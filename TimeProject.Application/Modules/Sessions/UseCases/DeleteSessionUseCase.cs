using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Modules.Sessions.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Sessions.UseCases;

public class DeleteSessionUseCase(
    ISessionRepository repository,
    IPeriodRepository periodRepository,
    ISyncRecordResumeUseCase syncRecordResumeUseCase) : IDeleteSessionUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var entity = repository.FindById(id, userId);

        if (entity == null)
            return result.SetError(SessionErrors.NotFound);

        periodRepository.DeleteByList(entity.Periods?.ToList() ?? []);
        repository.Delete(entity);

        var recordId = entity.RecordId;
        if (recordId != null)
            syncRecordResumeUseCase.Handle((int)recordId);

        return result.SetData(true);
    }
}