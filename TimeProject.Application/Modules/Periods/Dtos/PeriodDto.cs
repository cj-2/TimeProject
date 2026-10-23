using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Periods.Dtos;

public class PeriodDto
{
    [Required] public DateTimeOffset Start { get; set; }
    [Required] public DateTimeOffset End { get; set; }
}