using TimeProject.Application.Modules.Feedbacks.Dtos;
using TimeProject.Application.Modules.Feedbacks.Factories;
using TimeProject.Application.Modules.Feedbacks.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Feedbacks.UseCases;

public class SendFeedbackUseCase(IHookHandler hookHandler) : ISendFeedbackUseCase
{
    public ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, false, name, email, isVerified));

        return new CustomResult<bool> { Data = true };
    }
}