using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<IUser> Handle(CreateUserOAuthDto dto, string email);
}