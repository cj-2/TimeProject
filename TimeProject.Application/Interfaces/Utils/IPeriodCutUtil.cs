using TimeProject.Infrastructure.Database.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface IPeriodCutUtil
{
    Period Handle(Period entity, DateTimeOffset initDate, DateTimeOffset endDate);
    IEnumerable<Period> Handle(IEnumerable<Period> list, DateTimeOffset initDate, DateTimeOffset endDate);
}