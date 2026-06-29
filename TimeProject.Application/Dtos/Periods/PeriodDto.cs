using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Periods;

public class PeriodDto
{
    [Required] public DateTimeOffset Start { get; set; }
    [Required] public DateTimeOffset End { get; set; }
}