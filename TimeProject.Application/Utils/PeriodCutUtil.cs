using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class PeriodCutUtil : IPeriodCutUtil
{
    public Period Handle(Period entity, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        var period = new Period
        {
            RecordId = entity.RecordId, SessionId = entity.SessionId,
            Start = entity.Start > initDate ? entity.Start : initDate,
            End = entity.End > endDate ? entity.End = endDate.AddMilliseconds(-1) : entity.End
        };

        // to 0
        if (period.Start > period.End || period.Start > endDate)
            period.End = period.Start;

        return period;
    }

    public IEnumerable<Period> Handle(IEnumerable<Period> list, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        return list
            .Select(e => Handle(e, initDate, endDate))
            .Where(e => e.End > e.Start);
    }
}