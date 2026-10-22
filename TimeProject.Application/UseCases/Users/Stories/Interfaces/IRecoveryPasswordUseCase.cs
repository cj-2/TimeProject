using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface IRecoveryPasswordUseCase
{
    ICustomResult<bool> Handle(RecoveryPasswordDto dto);
}