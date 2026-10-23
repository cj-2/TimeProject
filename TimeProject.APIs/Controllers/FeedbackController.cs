using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Attributes;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Modules.Feedbacks.Dtos;
using TimeProject.Application.Modules.Feedbacks.Interfaces;
using TimeProject.Application.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("feedbacks")]
public class FeedbackController(
    ISendFeedbackUseCase sendFeedbackUseCase,
    ISendPublicFeedbackUseCase sendPublicFeedbackUseCase) : CustomController
{
    [HttpPost]
    [Authorize(Policy = "IsActive")]
    [UserChallenge]
    public ActionResult<bool> Send(FeedbackDto feedbackDto)
    {
        return HandleResponse(sendFeedbackUseCase
            .Handle(feedbackDto, UserClaimsUtil.Name(User), UserClaimsUtil.Email(User), UserClaimsUtil.IsVerified(User)));
    }

    [HttpPost("public")]
    [UserChallenge]
    public ActionResult<bool> SendPublic(PublicFeedbackDto feedbackDto)
    {
        return HandleResponse(sendPublicFeedbackUseCase.Handle(feedbackDto));
    }
}