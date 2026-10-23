using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, string email);
}