using TimeProject.Application.Modules.Minutes.Dtos;
using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Utils;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Modules.Sessions.Dtos;

public class SessionOutDto
{
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public int RecordId { get; set; }

    public string? Type { get; set; }
    public string? From { get; set; }

    public IEnumerable<PeriodOutDto>? Periods { get; set; }
    public IEnumerable<MinuteOutDto>? Minutes { get; set; }

    public string FormattedTime =>
        Periods.TimeSpanFromPeriods().Add(Minutes.TimeSpanFromMinutes()).StringFromTimeSpan();
}