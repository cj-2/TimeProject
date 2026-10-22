// using TimeProject.Api.Infrastructure.Interfaces;
// using TimeProject.Domain.Codes.UseCases;
// using TimeProject.Domain.User.UseCases;
// using Shared.General;
//
// namespace TimeProject.Api.Modules.User.UseCases;
//
// public class VerifyUserUseCase(
//     ISetIsUsedConfirmCodeUseCase setIsUsedConfirmCodeUseCase,
//     ISetIsVerifiedUserUseCase setIsVerifiedUserUseCase,
//     IValidateConfirmCodeUseCase validateConfirmCodeUseCase,
//     IHookHandler hookHandler
// ) : IVerifyUserUseCase
// {
//     public async Task<Result<bool>> Handle(int id, string email, string code)
//     {
//         var result = new Result<bool>();
//
//         var validateResult = await validateConfirmCodeUseCase.Handle(code, email);
//         if (validateResult.HasError) return result.SetError(validateResult.Message);
//
//         await setIsVerifiedUserUseCase.Handle(id, true);
//         await setIsUsedConfirmCodeUseCase.Handle(code);
//
//         await hookHandler.Send(HookTo.Users, $"O usuário com o e-mail <b>{email}</b> acabou de verificar sua conta.");
//
//         return result.SetData(true);
//     }
// }

