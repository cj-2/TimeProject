using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface ISetIsUsedConfirmCodeUseCase
{
    ICustomResult<bool> Handle(string id);
}