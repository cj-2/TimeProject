using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

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