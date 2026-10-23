using TimeProject.Application.Modules.Codes.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Codes.UseCases;

public class SetIsUsedConfirmCodeUseCase(IConfirmCodeRepository repository) : ISetIsUsedConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id)
    {
        var result = new CustomResult<bool>();
        var recoveryCode = repository.FindById(id);

        if (recoveryCode == null)
            return result.SetError(ConfirmCodeErrors.NotFound);

        recoveryCode.IsUsed = true;
        repository.Update(recoveryCode);

        return result.SetData(true);
    }
}