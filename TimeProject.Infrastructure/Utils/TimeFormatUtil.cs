using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.ObjectValues.Sessions;

namespace TimeProject.Infrastructure.Utils;

public static class TimeFormatUtil
{
    public static TimeSpan TimeSpanFromPeriods(IEnumerable<PeriodOutDto>? periods)
    {
        if (periods == null) return TimeSpan.Zero;

        return TimeSpanFromPeriods(periods
            .Select(p => new Period()
                { Start = p.Start, End = p.End }).ToList());
    }

    public static TimeSpan TimeSpanFromPeriods(IEnumerable<IPeriod>? periods)
    {
        if (periods == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;

        return periods
            .Where(e => e.End.HasValue)
            .Aggregate(total, (current, period) =>
                current.Add(period.End!.Value.Subtract(period.Start)));
    }

    public static TimeSpan TimeSpanFromMinutes(IEnumerable<IMinute>? timeMinutes)
    {
        if (timeMinutes == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;
        return timeMinutes.Aggregate(total, (current, tm) => current.Add(new TimeSpan(0, tm.Total, 0)));
    }

    public static TimeSpan TimeSpanFromSessions(IEnumerable<SessionOutDto>? sessions)
    {
        if (sessions == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;

        foreach (var session in sessions)
            total = total.Add(TimeSpanFromPeriods(session.Periods));

        return total;
    }

    public static TimeSpan TimeSpanFromSessions(IEnumerable<ISession>? recordSessions)
    {
        var total = TimeSpan.Zero;
        if (recordSessions == null) return total;

        foreach (var rs in (recordSessions as IList<Session>)!)
            if (rs.Periods != null && rs.Periods.Any())
                total = total.Add(TimeSpanFromPeriods(rs.Periods.ToList()));

        return total;
    }

    public static string StringFromTimeSpan(TimeSpan timeSpan)
    {
        var formatted = string.Empty;

        if (timeSpan.Days > 0)
            formatted += $"{timeSpan.Days}d ";
        if (timeSpan.Hours > 0)
            formatted += $"{timeSpan.Hours}h ";
        if (timeSpan.Minutes > 0)
            formatted += $"{timeSpan.Minutes}m ";
        if (timeSpan.Seconds > 0)
            formatted += $"{timeSpan.Seconds}s ";

        var result = formatted.Trim();
        return string.IsNullOrEmpty(result) ? "0s" : result;
    }

    public static string StringFromPeriods(IEnumerable<PeriodOutDto>? periods)
    {
        if (periods == null) return "0s";

        return StringFromTimeSpan(TimeSpanFromPeriods(periods
            .Select(p => new Period
                { Start = p.Start, End = p.End }).ToList()
        ));
    }

    public static string StringFromPeriods(IEnumerable<IPeriod>? periods)
    {
        return StringFromTimeSpan(TimeSpanFromPeriods(periods?.ToList()));
    }

    public static string StringFromSessions(IEnumerable<ISession>? sessions)
    {
        return StringFromTimeSpan(TimeSpanFromSessions(sessions));
    }

    public static double MinutesFromTimeSpan(TimeSpan timeSpan)
    {
        return timeSpan.TotalMinutes;
    }

    public static double HoursFromTimeSpan(TimeSpan timeSpan)
    {
        return timeSpan.TotalHours;
    }
}