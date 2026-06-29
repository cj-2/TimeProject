using TimeProject.Application.Dtos.Feedbacks;
using TimeProject.Application.Factories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Application.Shared;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Feedbacks;

public class SendFeedbackUseCase(IHookHandler hookHandler) : ISendFeedbackUseCase
{
    public ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, false, name, email, isVerified));

        return new CustomResult<bool> { Data = true };
    }
}