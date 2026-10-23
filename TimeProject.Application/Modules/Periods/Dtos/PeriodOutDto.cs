using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Modules.Periods.Dtos;

public class PeriodOutDto
{
    public int PeriodId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string FormattedTime => End.Subtract(Start).StringFromTimeSpan();
}