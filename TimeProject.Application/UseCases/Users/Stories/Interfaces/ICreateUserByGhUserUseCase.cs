using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public record EmailGh(string Email, bool Primary, bool Verified);

public interface ICreateUserByGhUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, IEnumerable<EmailGh> emails);
}