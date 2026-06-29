using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserByEmailUseCase
{
    ICustomResult<IUser> Handle(string email);
}