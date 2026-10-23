using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IRecoveryPasswordUseCase
{
    ICustomResult<bool> Handle(RecoveryPasswordDto dto);
}