using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Extensions;

public static class PeriodListExtensions
{
    public static TimeSpan TimeSpan(this IEnumerable<Period> list)
    {
        return list.Aggregate(System.TimeSpan.Zero, (total, p) => total + p.TimeSpan);
    } 
    
    public static TimeSpan TimeSpanCut(this IEnumerable<Period> list, DateTimeOffset limit)
    {
        return list.Aggregate(System.TimeSpan.Zero, (total, p) => total + p.TimeSpanCut(limit));
    } 
}