using TimeProject.Application.Factories;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Emails.Interfaces;
using TimeProject.Application.UseCases.Users.Queries.Interfaces;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Emails;

public class SendRecoveryEmailUseCase(
    ICreateConfirmCodeUseCase createConfirmCodeUseCase,
    ISetWasSentConfirmCodeUseCase setWasSentConfirmCodeUseCase,
    IUserRepository userRepository,
    IEmailHandler emailHandler,
    IHookHandler hookHandler
) : ISendRecoveryEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string recoveryUrl)
    {
        var result = new CustomResult<bool>();
        
        var user = userRepository.FindByEmail(email);
        if (user == null) return result.SetError(UserErrors.NotFound);

        var createRecoveryCodeResult = createConfirmCodeUseCase.Handle(user.UserId, ConfirmCodeType.Recovery);

        var recoveryCode = createRecoveryCodeResult.Data!;
        if (recoveryCode.WasSent) return result.SetError(ConfirmCodeErrors.CheckYourEmailInbox);

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
            return result.SetError(AuthErrors.SendEmailError);
        }

        return result.SetData(true);
    }
}