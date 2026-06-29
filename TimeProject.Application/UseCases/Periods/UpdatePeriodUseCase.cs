using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Application.UseCases.Periods;

public class UpdatePeriodUseCase(
    IPeriodRepository repository,
    ISyncRecordResumeUseCase syncRecordResumeUseCase,
    IPeriodValidateUtil periodValidateUtil
) : IUpdatePeriodUseCase
{
    public ICustomResult<IPeriod> Handle(int id, PeriodDto dto, int userId)
    {
        var result = new CustomResult<IPeriod>();

        periodValidateUtil.ValidateStartAndEnd(dto.Start, dto.End, result);
        if (result.HasError) return result;

        var period = repository.FindById(id, userId);
        if (period == null) return result.SetError(PeriodMessageErrors.NotFound);

        period.Start = dto.Start;
        period.End = dto.End;

        repository.Update(period);
        
        if (period.RecordId != null)
            syncRecordResumeUseCase.Handle((int)period.RecordId);

        return result.SetData(period);
    }
}