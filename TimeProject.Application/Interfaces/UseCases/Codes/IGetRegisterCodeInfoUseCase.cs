using TimeProject.Domain.Dtos.Codes;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface IGetRegisterCodeInfoUseCase
{
    ICustomResult<IConfirmCodeOutDto> Handle(int userId);
}