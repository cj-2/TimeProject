using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id);
}