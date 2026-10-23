using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Utils.Interfaces;

public interface IPeriodValidateUtil
{
    void ValidateStartAndEnd<T>(DateTimeOffset start, DateTimeOffset end, ICustomResult<T> customResult);

    bool HasMinSize(PeriodDto dto);
}