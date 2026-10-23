namespace TimeProject.Domain.Interfaces;

public interface ITimeSpanCalc
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset? End { get; }
    public TimeSpan TimeSpan { get; }
    public TimeSpan TimeSpanCut(DateTimeOffset limit);
}