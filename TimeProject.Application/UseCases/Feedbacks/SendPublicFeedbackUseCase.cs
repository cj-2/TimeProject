using TimeProject.Application.Dtos.Feedbacks;
using TimeProject.Application.Factories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Application.Shared;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Feedbacks;

public class SendPublicFeedbackUseCase(IHookHandler hookHandler) : ISendPublicFeedbackUseCase
{
    public ICustomResult<bool> Handle(PublicFeedbackDto dto)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, true, dto.Name, dto.Email));

        return new CustomResult<bool> { Data = true };
    }
}