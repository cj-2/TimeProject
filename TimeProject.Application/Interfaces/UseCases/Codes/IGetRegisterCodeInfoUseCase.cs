using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Codes;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface IGetRegisterCodeInfoUseCase
{
    ICustomResult<ConfirmCodeOutDto> Handle(int userId);
}