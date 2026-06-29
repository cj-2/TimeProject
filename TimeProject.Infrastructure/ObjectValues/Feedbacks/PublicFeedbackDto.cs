using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Feedbacks;

public class PublicFeedbackDto : FeedbackDto
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Email { get; set; } = "";
}