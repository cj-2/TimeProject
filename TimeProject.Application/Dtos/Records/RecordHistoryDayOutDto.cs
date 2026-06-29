using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Sessions;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Dtos.Records;

public class RecordHistoryDayOutDto
{
    public DateTime Date { get; set; }

    public IEnumerable<PeriodOutDto>? Periods { get; set; }
    public IEnumerable<SessionOutDto>? Sessions { get; set; }
    public IEnumerable<IMinute>? Minutes { get; set; }

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