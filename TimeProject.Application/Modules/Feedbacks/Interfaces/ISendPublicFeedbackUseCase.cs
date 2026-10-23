using TimeProject.Application.Modules.Feedbacks.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Feedbacks.Interfaces;

public interface ISendPublicFeedbackUseCase
{
    ICustomResult<bool> Handle(PublicFeedbackDto dto);
}