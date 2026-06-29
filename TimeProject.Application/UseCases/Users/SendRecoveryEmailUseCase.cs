using TimeProject.Application.Factories;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class SendRecoveryEmailUseCase(
    IGetUserByEmailUseCase getUserByEmailUseCase,
    ICreateConfirmCodeUseCase createConfirmCodeUseCase,
    ISetWasSentConfirmCodeUseCase setWasSentConfirmCodeUseCase,
    IEmailHandler emailHandler,
    IHookHandler hookHandler
) : ISendRecoveryEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string recoveryUrl)
    {
        var result = new CustomResult<bool>();

        var findUserResult = getUserByEmailUseCase.Handle(email);
        if (findUserResult.HasError) return result.SetError(findUserResult.Message);

        var user = findUserResult.Data!;

        var createRecoveryCodeResult = createConfirmCodeUseCase.Handle(user.UserId, ConfirmCodeType.Recovery);

        var recoveryCode = createRecoveryCodeResult.Data!;
        if (recoveryCode.WasSent) return result.SetError(ConfirmCodeMessageErrors.CheckYourEmailInbox);

        try
        {
            emailHandler.Send(RecoveryEmailFactory.Create(
                email,
                recoveryUrl + recoveryCode.CodeId,
                recoveryCode.Expiration
            ));

            setWasSentConfirmCodeUseCase.Handle(recoveryCode.CodeId);
        }
        catch
        {
            hookHandler.SendError($"Não foi possível enviar o e-mail de recuperação para:\n<b>{user.Email}</b>");
            return result.SetError(AuthMessageErrors.SendEmailError);
        }

        return result.SetData(true);
    }
}