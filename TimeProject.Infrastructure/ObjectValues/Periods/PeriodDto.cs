using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Periods;

public class PeriodDto
{
    [Required] public DateTimeOffset Start { get; set; }
    [Required] public DateTimeOffset End { get; set; }
}