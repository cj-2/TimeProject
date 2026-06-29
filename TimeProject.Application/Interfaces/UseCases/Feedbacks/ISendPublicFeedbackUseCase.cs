using TimeProject.Application.Dtos.Feedbacks;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Feedbacks;

public interface ISendPublicFeedbackUseCase
{
    ICustomResult<bool> Handle(PublicFeedbackDto dto);
}