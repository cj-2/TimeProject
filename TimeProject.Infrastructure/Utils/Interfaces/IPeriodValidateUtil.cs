using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Infrastructure.Utils.Interfaces;

public interface IPeriodValidateUtil
{
    void ValidateStartAndEnd<T>(DateTimeOffset start, DateTimeOffset end, ICustomResult<T> customResult);

    bool HasMinSize(PeriodDto dto);
}