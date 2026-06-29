using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Dtos.Periods;

public class PeriodListDto
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SessionType? Type { get; set; }
    public string? From { get; set; } = string.Empty;
    [Required] public IList<PeriodDto> Periods { get; set; } = null!;
}