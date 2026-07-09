namespace TimeProject.Domain.Entities;

public class Period
{
    public int PeriodId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset? End { get; set; }

    public int RecordId { get; set; }
    public int SessionId { get; set; }
    public int? UserId { get; set; }

    public Record? Record { get; set; }
    public Session? Session { get; set; }
}