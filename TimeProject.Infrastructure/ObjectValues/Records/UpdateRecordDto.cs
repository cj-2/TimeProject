using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Records;

public class UpdateRecordDto
{
    [MaxLength(120)] public string? Title { get; set; }
    [MaxLength(240)] public string? Description { get; set; }
    [MaxLength(120)] public string? ExternalLink { get; set; }
    [MaxLength(36)] public string Code { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
}