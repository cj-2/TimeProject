using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.UseCases.Periods;

public class UpdatePeriodUseCase(
    IPeriodRepository repository,
    IPeriodValidateUtil periodValidateUtil
) : IUpdatePeriodUseCase
{
    public ICustomResult<Period> Handle(int id, PeriodDto dto, int userId)
    {
        var result = new CustomResult<Period>();

        periodValidateUtil.ValidateStartAndEnd(dto.Start, dto.End, result);
        if (result.HasError) return result;

        var period = repository.FindById(id, userId);
        if (period == null) return result.SetError(PeriodErrors.NotFound);

        period.DefineRange(dto.Start, dto.End);
        repository.Update(period);

        return result.SetData(period);
    }
}