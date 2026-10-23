using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, string email);
}