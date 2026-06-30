using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.UseCases.Sessions;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Sessions;

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
            return result.SetError(SessionMessageErrors.NotFound);

        periodRepository.DeleteByList(entity.Periods?.ToList() ?? []);
        repository.Delete(entity);

        var recordId = entity.RecordId;
        if (recordId != null)
            syncRecordResumeUseCase.Handle((int)recordId);

        return result.SetData(true);
    }
}