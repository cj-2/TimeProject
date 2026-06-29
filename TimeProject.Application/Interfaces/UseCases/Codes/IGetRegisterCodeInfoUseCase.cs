using TimeProject.Application.Dtos.Codes;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface IGetRegisterCodeInfoUseCase
{
    ICustomResult<ConfirmCodeOutDto> Handle(int userId);
}