using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.Utils;

public interface IPeriodValidateUtil
{
    void ValidateStartAndEnd<T>(DateTimeOffset start, DateTimeOffset end, ICustomResult<T> customResult);

    bool HasMinSize(PeriodDto dto);
}