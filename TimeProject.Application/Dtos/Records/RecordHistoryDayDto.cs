using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Records;

public class RecordHistoryDayDto
{
    public DateTime Date { get; set; }
    public DateTime InitDate { get; set; }
    public DateTime EndDate { get; set; }
    public IEnumerable<Period>? Periods { get; set; }
    public IEnumerable<Minute>? Minutes { get; set; }
    public IEnumerable<Session>? Sessions { get; set; }
}