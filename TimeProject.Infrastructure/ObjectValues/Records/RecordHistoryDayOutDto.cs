using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.ObjectValues.Sessions;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Infrastructure.ObjectValues.Records;

public class RecordHistoryDayOutDto
{
    public DateTime Date { get; set; }

    public IEnumerable<PeriodOutDto>? Periods { get; set; }
    public IEnumerable<SessionOutDto>? Sessions { get; set; }
    public IEnumerable<IMinute>? Minutes { get; set; }

    private TimeSpan TimeSpanPeriods => TimeFormatUtil.TimeSpanFromPeriods(Periods);
    private TimeSpan TimeSpanSessions => TimeFormatUtil.TimeSpanFromSessions(Sessions);
    private TimeSpan TimeSpanMinutes => TimeFormatUtil.TimeSpanFromMinutes(Minutes);

    private TimeSpan TimeSpanSum => TimeSpanPeriods.Add(TimeSpanSessions).Add(TimeSpanMinutes);

    public string FormattedTime => TimeFormatUtil.StringFromTimeSpan(TimeSpanSum);
    public double TimeInMinutes => TimeFormatUtil.MinutesFromTimeSpan(TimeSpanSum);
    public double TimeInHours => TimeFormatUtil.HoursFromTimeSpan(TimeSpanSum);
    public string PeriodsFormattedTime => TimeFormatUtil.StringFromPeriods(Periods);
    public string MinutesFormattedTime => TimeFormatUtil.StringFromTimeSpan(TimeSpanMinutes);
    public string SessionsFormattedTime => TimeFormatUtil.StringFromTimeSpan(TimeSpanSessions);
}