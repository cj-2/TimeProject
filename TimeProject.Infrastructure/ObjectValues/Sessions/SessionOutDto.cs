using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.Infrastructure.ObjectValues.Sessions;

public class SessionOutDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RecordId { get; set; }

    public string? Type { get; set; }
    public string? From { get; set; }

    public IEnumerable<PeriodOutDto>? Periods { get; set; }

    public string FormattedTime => TimeFormatUtil.StringFromPeriods(Periods);
}