using TimeProject.Domain.Errors;
using TimeProject.Domain.Interfaces;

namespace TimeProject.Domain.Entities;

public class Period : ITimeSpanCalc
{
    public Period(DateTimeOffset start, DateTimeOffset? end)
    {
        DefineRange(start, end);
    }

    private DateTimeOffset _start;
    private DateTimeOffset? _end;

    public int PeriodId { get; init; }

    public DateTimeOffset Start
    {
        get => _start;
        init => _start = value;
    }

    public DateTimeOffset? End
    {
        get => _end;
        init => _end = value;
    }

    public int RecordId { get; init; }
    public int SessionId { get; set; }
    public int? UserId { get; set; }

    public Record? Record { get; set; }
    public Session? Session { get; set; }

    public TimeSpan TimeSpan => _end?.Subtract(_start) ?? TimeSpan.Zero;

    public TimeSpan TimeSpanCut(DateTimeOffset limit) => limit > _end ? TimeSpan : limit.Subtract(_start);

    public void DefineRange(DateTimeOffset start, DateTimeOffset? end)
    {
        if (end != null && start >= end)
            throw new DomainException(PeriodErrors.EndDateIsBiggerThenStartDate);

        _start = start;
        _end = end;
    }
}