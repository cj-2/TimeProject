using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Codes;

public class SetIsUsedConfirmCodeUseCase(IConfirmCodeRepository repository) : ISetIsUsedConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id)
    {
        var result = new CustomResult<bool>();
        var recoveryCode = repository.FindById(id);

        if (recoveryCode == null)
            return result.SetError(ConfirmCodeMessageErrors.NotFound);

        recoveryCode.IsUsed = true;
        repository.Update(recoveryCode);

        return result.SetData(true);
    }
}