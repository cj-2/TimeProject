using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared;

namespace TimeProject.Application.Modules.Users.UseCases;

public class RecoveryPasswordUseCase(
    ICreateOrUpdateUserPasswordByEmailUseCase createOrUpdateUserPasswordByEmailUseCase,
    ISetIsUsedConfirmCodeUseCase setIsUsedConfirmCodeUseCase,
    IValidateConfirmCodeUseCase validateConfirmCodeUseCase
) : IRecoveryPasswordUseCase
{
    public ICustomResult<bool> Handle(RecoveryPasswordDto dto)
    {
        var result = new CustomResult<bool>();

        var validateConfirmCodeResult = validateConfirmCodeUseCase.Handle(dto.Code, dto.Email);
        if (validateConfirmCodeResult.HasError) return result.SetError(validateConfirmCodeResult.Message);

        var updatePasswordResult = createOrUpdateUserPasswordByEmailUseCase.Handle(dto.Email, dto.Password);
        if (updatePasswordResult.HasError) return result.SetError(updatePasswordResult.Message);

        setIsUsedConfirmCodeUseCase.Handle(dto.Code);

        return result.SetData(true);
    }
}