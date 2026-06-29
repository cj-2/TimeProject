using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Codes;

public class SetWasSentConfirmCodeUseCase(IConfirmCodeRepository repository) : ISetWasSentConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id, bool wasSent = true)
    {
        var result = new CustomResult<bool>();
        var recoveryCode = repository.FindById(id);

        if (recoveryCode == null)
            return result.SetError(ConfirmCodeMessageErrors.NotFound);

        recoveryCode.WasSent = wasSent;
        repository.Update(recoveryCode);

        return result.SetData(true);
    }
}