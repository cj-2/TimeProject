using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public record EmailGh(string Email, bool Primary, bool Verified);

public interface ICreateUserByGhUserUseCase
{
    ICustomResult<IUser> Handle(CreateUserOAuthDto dto, IEnumerable<EmailGh> emails);
}