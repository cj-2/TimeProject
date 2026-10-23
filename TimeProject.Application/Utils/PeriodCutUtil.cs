using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class PeriodCutUtil : IPeriodCutUtil
{
    public Period Handle(Period entity, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        var period = new Period(
            entity.Start > initDate ? entity.Start : initDate,
            entity.End > endDate ? endDate.AddMilliseconds(-1) : entity.End
        )
        {
            RecordId = entity.RecordId, SessionId = entity.SessionId,
        };

        return period;
    }

    public IEnumerable<Period> Handle(IEnumerable<Period> list, DateTimeOffset initDate, DateTimeOffset endDate)
    {
        return list
            .Select(e => Handle(e, initDate, endDate))
            .Where(e => e.End > e.Start);
    }
}