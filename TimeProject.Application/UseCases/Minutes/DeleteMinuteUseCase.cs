using TimeProject.Application.Interfaces.UseCases.Minutes;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Minutes;

public class DeleteMinuteUseCase(
    IMinuteRepository repository,
    ISyncRecordResumeUseCase syncRecordResumeUseCase
) : IDeleteMinuteUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();

        var timeMinute = repository.FindById(id, userId);
        if (timeMinute == null) return result.SetError(MinuteMessageErrors.NotFound);

        var data = repository.Delete(timeMinute);
        if (timeMinute.RecordId != null)
            syncRecordResumeUseCase.Handle((int)timeMinute.RecordId);

        return result.SetData(data);
    }
}