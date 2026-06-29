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

public class SendRegisterEmailUseCase(
    ICreateConfirmCodeUseCase createConfirmCodeUseCase,
    IGetUserByEmailUseCase getUserByEmailUseCase,
    ISetWasSentConfirmCodeUseCase setWasSentConfirmCodeUseCase,
    IEmailHandler emailHandler,
    IHookHandler hookHandler
) : ISendRegisterEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string verifyUrl)
    {
        var result = new CustomResult<bool>();

        var findUserResult = getUserByEmailUseCase.Handle(email);
        if (findUserResult.HasError) return result.SetError(findUserResult.Message);

        var user = findUserResult.Data!;

        var registerCodeResult = createConfirmCodeUseCase.Handle((int)user.UserId!, ConfirmCodeType.Register);
        if (registerCodeResult.HasError) return result.SetError(registerCodeResult.Message);

        var registerCode = registerCodeResult.Data!;
        if (registerCode.WasSent) return result.SetError(ConfirmCodeMessageErrors.CheckYourEmailInbox);

        try
        {
            emailHandler.Send(RegisterEmailFactory.Create(
                email,
                verifyUrl + registerCode.CodeId,
                registerCode.Expiration
            ));

            setWasSentConfirmCodeUseCase.Handle(registerCode.CodeId);
        }
        catch
        {
            hookHandler.SendError($"Não foi possível enviar o e-mail de verificação para:\n<b>{user.Email}</b>");
            return result.SetError(AuthMessageErrors.SendEmailError);
        }

        return result.SetData(true);
    }
}