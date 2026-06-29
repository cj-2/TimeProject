using System.ComponentModel.DataAnnotations;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Infrastructure.ObjectValues.Records;

public class CreateRecordDto
{
    [MaxLength(120)] public string? Name { get; set; }
    [MaxLength(240)] public string? Description { get; set; }
    [MaxLength(120)] public string? ExternalLink { get; set; }
    [MaxLength(36)] public string? Code { get; set; }

    [MaxLength(10)] public SessionType? SessionType { get; set; }
    [MaxLength(15)] public string? SessionFrom { get; set; }

    public int? CategoryId { get; set; }

    [Required] public IList<PeriodDto>? Periods { get; set; }
}