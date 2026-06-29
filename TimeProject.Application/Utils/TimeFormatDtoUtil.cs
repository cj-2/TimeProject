using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Sessions;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Utils;

public static class TimeFormatDtoUtil
{
    public static TimeSpan TimeSpanFromPeriods(this IEnumerable<PeriodOutDto>? periods)
    {
        if (periods == null) return TimeSpan.Zero;

        return periods
            .Select(p => new Period { Start = p.Start, End = p.End })
            .TimeSpanFromPeriods();
    }

    public static TimeSpan TimeSpanFromSessions(this IEnumerable<SessionOutDto>? sessions)
    {
        if (sessions == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;

        foreach (var session in sessions)
        {
            total = total.Add(session.Periods.TimeSpanFromPeriods());
        }

        return total;
    }
    
    public static TimeSpan TimeSpanFromMinutes(this IEnumerable<MinuteOutDto>? timeMinutes)
    {
        if (timeMinutes == null) return TimeSpan.Zero;
        var total = TimeSpan.Zero;
        return timeMinutes.Aggregate(total, (current, tm) => current.Add(new TimeSpan(0, tm.Total, 0)));
    }

    public static string StringFromPeriods(this IEnumerable<PeriodOutDto>? periods)
    {
        if (periods == null) return "0s";

        return periods
            .Select(p => new Period { Start = p.Start, End = p.End })
            .TimeSpanFromPeriods()
            .StringFromTimeSpan();
    }
}