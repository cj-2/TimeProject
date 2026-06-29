using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface ISetIsUsedConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id);
}