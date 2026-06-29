using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Infrastructure.Utils;

public class PeriodValidateUtil : IPeriodValidateUtil
{
    public void ValidateStartAndEnd<T>(
        DateTimeOffset start,
        DateTimeOffset end,
        ICustomResult<T> customResult
    )
    {
        if (start.CompareTo(end) > 0)
            customResult.SetError(PeriodMessageErrors.EndDateIsBiggerThenStartDate);
    }

    public bool HasMinSize(PeriodDto dto)
    {
        var time = dto.End.Subtract(dto.Start);
        return time.TotalSeconds > 2;
    }
}