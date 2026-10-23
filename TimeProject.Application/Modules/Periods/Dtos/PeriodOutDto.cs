using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Modules.Periods.Dtos;

public class PeriodOutDto
{
    public int PeriodId { get; init; }
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset? End { get; init; }
    public string FormattedTime => End?.Subtract(Start).StringFromTimeSpan() ?? "";

    public static PeriodOutDto Map(Period period)
    {
        return new PeriodOutDto
        {
            PeriodId = period.PeriodId,
            Start = period.Start,
            End = period.End
        };
    }
}