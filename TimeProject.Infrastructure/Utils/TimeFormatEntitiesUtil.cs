using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Utils;

public static class TimeFormatEntitiesUtil
{
    public static TimeSpan TimeSpanFromPeriods(this IEnumerable<Period>? periods)
    {
        if (periods == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;

        return periods
            .Where(e => e.End.HasValue)
            .Aggregate(total, (current, period) =>
                current.Add(period.End!.Value.Subtract(period.Start)));
    }

    public static TimeSpan TimeSpanFromMinutes(this IEnumerable<Minute>? timeMinutes)
    {
        if (timeMinutes == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;
        return timeMinutes.Aggregate(total, (current, tm) => current.Add(new TimeSpan(0, tm.Total, 0)));
    }
    
    public static TimeSpan TimeSpanFromSessions(this IEnumerable<Session>? recordSessions)
    {
        var total = TimeSpan.Zero;
        if (recordSessions == null) return total;

        foreach (var rs in recordSessions)
            if (rs.Periods != null && rs.Periods.Any())
                total = total.Add(TimeSpanFromPeriods(rs.Periods.ToList()));

        return total;
    }

    public static string StringFromTimeSpan(this TimeSpan timeSpan)
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
    
    public static string StringFromPeriods(this IEnumerable<Period>? periods)
    {
        return StringFromTimeSpan(TimeSpanFromPeriods(periods?.ToList()));
    }

    public static string StringFromSessions(this IEnumerable<Session>? sessions)
    {
        return StringFromTimeSpan(TimeSpanFromSessions(sessions));
    }

    public static double MinutesFromTimeSpan(this TimeSpan timeSpan)
    {
        return timeSpan.TotalMinutes;
    }

    public static double HoursFromTimeSpan(this TimeSpan timeSpan)
    {
        return timeSpan.TotalHours;
    }
}