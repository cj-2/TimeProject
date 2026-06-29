using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Feedbacks;

public class FeedbackDto
{
    [Required] public string Message { get; set; } = "";
}