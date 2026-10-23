using TimeProject.Application.Modules.Codes.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Codes.UseCases;

public class SetWasSentConfirmCodeUseCase(IConfirmCodeRepository repository) : ISetWasSentConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id, bool wasSent = true)
    {
        var result = new CustomResult<bool>();
        var recoveryCode = repository.FindById(id);

        if (recoveryCode == null)
            return result.SetError(ConfirmCodeErrors.NotFound);

        recoveryCode.WasSent = wasSent;
        repository.Update(recoveryCode);

        return result.SetData(true);
    }
}