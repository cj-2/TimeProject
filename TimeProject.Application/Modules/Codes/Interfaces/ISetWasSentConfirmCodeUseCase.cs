using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Codes.Interfaces;

public interface ISetWasSentConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id, bool wasSent = true);
}