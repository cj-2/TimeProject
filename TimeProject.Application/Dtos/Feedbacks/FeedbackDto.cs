using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Feedbacks;

public class FeedbackDto
{
    [Required] public string Message { get; set; } = "";
}