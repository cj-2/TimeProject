using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Utils;

public class PeriodValidateUtil : IPeriodValidateUtil
{
    public void ValidateStartAndEnd<T>(
        DateTimeOffset start,
        DateTimeOffset end,
        ICustomResult<T> customResult
    )
    {
        if (start.CompareTo(end) > 0)
            customResult.SetError(PeriodErrors.EndDateIsBiggerThenStartDate);
    }

    public bool HasMinSize(PeriodDto dto)
    {
        var time = dto.End.Subtract(dto.Start);
        return time.TotalSeconds > 2;
    }
}