using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.UseCases.Periods;

public class CreatePeriodUseCase(
    IPeriodRepository repository,
    IGetRecordByIdUseCase getRecordByIdUseCase,
    ISyncRecordResumeUseCase syncRecordResumeUseCase,
    IPeriodValidateUtil periodValidateUtil
) : ICreatePeriodUseCase
{
    public ICustomResult<IPeriod> Handle(CreatePeriodDto data, int userId)
    {
        var result = new CustomResult<IPeriod>();

        periodValidateUtil.ValidateStartAndEnd(data.Start, data.End, result);
        if (result.HasError) return result;

        if (data.Start.CompareTo(data.End) > 0)
            return result.SetError(PeriodMessageErrors.EndDateIsBiggerThenStartDate);

        var findTrResult = getRecordByIdUseCase.Handle(data.RecordId, userId);
        if (findTrResult.HasError) return result.SetError(findTrResult.Message);

        var period = repository.Create(new Period
            {
                UserId = userId,
                RecordId = data.RecordId,
                Start = data.Start,
                End = data.End
            }
        );

        if (period.RecordId != null)
            syncRecordResumeUseCase.Handle((int)period.RecordId);

        return result.SetData(period);
    }
}