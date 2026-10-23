using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Extensions;

public static class MinuteListExtensions
{
    public static TimeSpan TimeSpan(this IEnumerable<Minute> list)
    {
        return list.Aggregate(System.TimeSpan.Zero, (total, p) => total + p.TimeSpan);
    } 
    
    public static TimeSpan TimeSpanCut(this IEnumerable<Minute> list, DateTimeOffset limit)
    {
        return list.Aggregate(System.TimeSpan.Zero, (total, p) => total + p.TimeSpanCut(limit));
    } 
}