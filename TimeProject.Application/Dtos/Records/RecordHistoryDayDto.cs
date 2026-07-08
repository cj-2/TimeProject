using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Records;

public class RecordHistoryDayDto
{
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset InitDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<Period>? Periods { get; set; }
    public List<Minute>? Minutes { get; set; }
    public List<Session>? Sessions { get; set; }
}