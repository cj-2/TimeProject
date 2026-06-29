using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ISendRegisterEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string verifyUrl);
}