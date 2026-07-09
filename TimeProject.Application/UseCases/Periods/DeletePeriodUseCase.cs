using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Periods;

public class DeletePeriodUseCase(IPeriodRepository repository, ISyncRecordResumeUseCase syncRecordResumeUseCase)
    : IDeletePeriodUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var period = repository.FindById(id, userId);

        if (period == null)
            return result.SetError(PeriodMessageErrors.NotFound);

        var data = repository.Delete(period);
        syncRecordResumeUseCase.Handle((int)period.RecordId);

        return result.SetData(data);
    }
}