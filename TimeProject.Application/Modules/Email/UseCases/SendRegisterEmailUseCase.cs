using TimeProject.Application.Modules.Codes.Interfaces;
using TimeProject.Application.Modules.Email.Factories;
using TimeProject.Application.Modules.Email.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Errors;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Email.UseCases;

public class SendRegisterEmailUseCase(
    ICreateConfirmCodeUseCase createConfirmCodeUseCase,
    ISetWasSentConfirmCodeUseCase setWasSentConfirmCodeUseCase,
    IUserRepository userRepository,
    IEmailHandler emailHandler,
    IHookHandler hookHandler
) : ISendRegisterEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string verifyUrl)
    {
        var result = new CustomResult<bool>();

        var user = userRepository.FindByEmail(email);
        if (user == null) return result.SetError(UserErrors.NotFound);
        
        var registerCodeResult = createConfirmCodeUseCase.Handle((int)user.UserId!, ConfirmCodeType.Register);
        if (registerCodeResult.HasError) return result.SetError(registerCodeResult.Message);

        var registerCode = registerCodeResult.Data!;
        if (registerCode.WasSent) return result.SetError(ConfirmCodeErrors.CheckYourEmailInbox);

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
            return result.SetError(AuthErrors.SendEmailError);
        }

        return result.SetData(true);
    }
}