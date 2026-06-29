using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface IValidateConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id, string email);
}