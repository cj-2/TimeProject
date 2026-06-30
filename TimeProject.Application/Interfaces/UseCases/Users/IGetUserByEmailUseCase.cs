using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserByEmailUseCase
{
    ICustomResult<User> Handle(string email);
}