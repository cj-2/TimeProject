using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils.Interfaces;

public interface IPeriodCutUtil
{
    Period Handle(Period entity, DateTimeOffset initDate, DateTimeOffset endDate);
    IEnumerable<Period> Handle(IEnumerable<Period> list, DateTimeOffset initDate, DateTimeOffset endDate);
}