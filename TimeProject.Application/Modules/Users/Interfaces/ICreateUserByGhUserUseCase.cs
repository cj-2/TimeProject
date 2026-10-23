using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Users.Interfaces;

public record EmailGh(string Email, bool Primary, bool Verified);

public interface ICreateUserByGhUserUseCase
{
    ICustomResult<User> Handle(CreateUserOAuthDto dto, IEnumerable<EmailGh> emails);
}