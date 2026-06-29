using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public record EmailGh(string Email, bool Primary, bool Verified);

public interface ICreateUserByGhUserUseCase
{
    ICustomResult<IUser> Handle(ICreateUserOAtuhDto dto, IEnumerable<EmailGh> emails);
}