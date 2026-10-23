using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Email.Interfaces;

public interface ISendRegisterEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string verifyUrl);
}