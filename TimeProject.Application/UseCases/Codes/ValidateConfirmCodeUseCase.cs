using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Codes;

public class ValidateConfirmCodeUseCase(IConfirmCodeRepository repository) : IValidateConfirmCodeUseCase
{
    public ICustomResult<bool> Handle(string id, string email)
    {
        var result = new CustomResult<bool>();

        var code = repository.FindByIdAndEmail(id, email);

        if (code == null) return result.SetError(ConfirmCodeMessageErrors.NotFound);

        if (code is { IsUsed: true } || DateTime.Now.ToUniversalTime() > code.Expiration)
            return result.SetError(ConfirmCodeMessageErrors.IsUsedOrExpired);

        return result.SetData(true);
    }
}