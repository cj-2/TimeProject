using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IDisableUserUseCase
{
    ICustomResult<bool> Handle(int id, DisableUserDto dto);
}