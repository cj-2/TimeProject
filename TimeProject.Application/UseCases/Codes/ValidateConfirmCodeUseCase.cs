using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.UseCases.Codes;

public class ValidateConfirmCodeUseCase(IConfirmCodeRepository repository) : IValidateConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id, string email)
    {
        var result = new CustomResult<bool>();

        var code = repository.FindByIdAndEmail(id, email);

        if (code == null) return result.SetError(ConfirmCodeErrors.NotFound);

        if (code is { IsUsed: true } || DateTime.Now.ToUniversalTime() > code.Expiration)
            return result.SetError(ConfirmCodeErrors.IsUsedOrExpired);

        return result.SetData(true);
    }
}