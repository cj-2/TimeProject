using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IUpdateUserRoleUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto);
}