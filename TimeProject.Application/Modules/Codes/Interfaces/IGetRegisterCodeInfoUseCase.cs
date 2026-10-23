using TimeProject.Application.Modules.Codes.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Codes.Interfaces;

public interface IGetRegisterCodeInfoUseCase
{
    ICustomResult<ConfirmCodeOutDto> Handle(int userId);
}