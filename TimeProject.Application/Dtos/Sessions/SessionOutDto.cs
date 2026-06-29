using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Utils;

namespace TimeProject.Application.Dtos.Sessions;

public class SessionOutDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecordId { get; set; }

    public string? Type { get; set; }
    public string? From { get; set; }

    public IEnumerable<PeriodOutDto>? Periods { get; set; }

    public string FormattedTime => Periods.StringFromPeriods();
}