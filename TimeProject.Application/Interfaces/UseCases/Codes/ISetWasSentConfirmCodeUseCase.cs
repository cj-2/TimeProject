using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface ISetWasSentConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id, bool wasSent = true);
}