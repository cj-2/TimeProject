using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Modules.Periods.Interfaces;
using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Periods.UseCases;

public class CreatePeriodUseCase(
    IPeriodRepository repository,
    IGetRecordByIdUseCase getRecordByIdUseCase,
    IPeriodValidateUtil periodValidateUtil
) : ICreatePeriodUseCase
{
    public ICustomResult<Period> Handle(CreatePeriodDto data, int userId)
    {
        var result = new CustomResult<Period>();

        periodValidateUtil.ValidateStartAndEnd(data.Start, data.End, result);
        if (result.HasError) return result;

        if (data.Start.CompareTo(data.End) > 0)
            return result.SetError(PeriodErrors.EndDateIsBiggerThenStartDate);

        var findTrResult = getRecordByIdUseCase.Handle(data.RecordId, userId);
        if (findTrResult.HasError) return result.SetError(findTrResult.Message);

        var period = repository.Create(new Period(data.Start, data.End)
            {
                UserId = userId,
                RecordId = data.RecordId,
            }
        );

        return result.SetData(period);
    }
}