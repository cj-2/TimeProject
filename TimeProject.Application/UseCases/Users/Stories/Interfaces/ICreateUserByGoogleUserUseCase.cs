using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, string email);
}