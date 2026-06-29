using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserByEmailUseCase
{
    ICustomResult<IUser> Handle(string email);
}