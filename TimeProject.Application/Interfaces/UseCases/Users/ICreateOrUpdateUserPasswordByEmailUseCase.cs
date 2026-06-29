using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateOrUpdateUserPasswordByEmailUseCase
{
    ICustomResult<bool> Handle(string email, string password);
}