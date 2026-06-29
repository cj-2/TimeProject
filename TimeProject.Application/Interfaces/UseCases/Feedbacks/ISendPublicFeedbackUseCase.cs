using TimeProject.Domain.Dtos.Feedbacks;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Feedbacks;

public interface ISendPublicFeedbackUseCase
{
    ICustomResult<bool> Handle(IPublicFeedbackDto feedbackDto);
}