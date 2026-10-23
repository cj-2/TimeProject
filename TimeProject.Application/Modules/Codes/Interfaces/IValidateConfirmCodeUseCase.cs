using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Codes.Interfaces;

public interface IValidateConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id, string email);
}