using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateUserByGoogleUserUseCase
{
    ICustomResult<IUser> Handle(ICreateUserOAtuhDto dto, string email);
}