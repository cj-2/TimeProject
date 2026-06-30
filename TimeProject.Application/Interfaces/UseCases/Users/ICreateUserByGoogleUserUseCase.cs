using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, string email);
}