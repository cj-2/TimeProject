using TimeProject.Infrastructure.Utils;

namespace TimeProject.Application.Dtos.Minutes;

public class MinuteOutDto
{
    public int MinuteId { get; set; }
    public DateTimeOffset Date { get; set; }
    public int Total { get; set; }
    public string FormattedTime => new TimeSpan(0, Total, 0).StringFromTimeSpan();
}