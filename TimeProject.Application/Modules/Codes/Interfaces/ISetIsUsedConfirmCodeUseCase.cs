using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Codes.Interfaces;

public interface ISetIsUsedConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id);
}