using System.ComponentModel.DataAnnotations;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Dtos.Periods;

public class PeriodListDto
{
    public SessionType? Type { get; set; }
    public string? From { get; set; } = string.Empty;
    [Required] public IList<PeriodDto> Periods { get; set; } = null!;
}