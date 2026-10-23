using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Modules.Minutes.Dtos;
using TimeProject.Application.Modules.Sessions.Dtos;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Modules.Records.Dtos;

public class RecordHistoryDayOutDto
{
    public DateTimeOffset Date { get; set; }

    public List<PeriodOutDto>? Periods { get; set; }
    public List<SessionOutDto>? Sessions { get; set; }
    public List<MinuteOutDto>? Minutes { get; set; }

    private TimeSpan TimeSpanPeriods => Periods.TimeSpanFromPeriods();
    private TimeSpan TimeSpanSessions => Sessions.TimeSpanFromSessions();
    private TimeSpan TimeSpanMinutes => Minutes.TimeSpanFromMinutes();

    private TimeSpan TimeSpanSum => TimeSpanPeriods.Add(TimeSpanSessions).Add(TimeSpanMinutes);

    public string FormattedTime => TimeSpanSum.StringFromTimeSpan();
    public double TimeInMinutes => TimeSpanSum.MinutesFromTimeSpan();
    public double TimeInHours => TimeSpanSum.HoursFromTimeSpan();
    public string PeriodsFormattedTime => Periods.StringFromPeriods();
    public string MinutesFormattedTime => TimeSpanMinutes.StringFromTimeSpan();
    public string SessionsFormattedTime => TimeSpanSessions.StringFromTimeSpan();
}