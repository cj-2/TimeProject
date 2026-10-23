using TimeProject.Application.Modules.Feedbacks.Dtos;
using TimeProject.Application.Modules.Feedbacks.Factories;
using TimeProject.Application.Modules.Feedbacks.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Feedbacks.UseCases;

public class SendPublicFeedbackUseCase(IHookHandler hookHandler) : ISendPublicFeedbackUseCase
{
    public ICustomResult<bool> Handle(PublicFeedbackDto dto)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, true, dto.Name, dto.Email));

        return new CustomResult<bool> { Data = true };
    }
}