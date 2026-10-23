// using TimeProject.Domain.User;
// using TimeProject.Domain.User.UseCases;
// using TimeProject.Api.Infrastructure.Errors;
// using TimeProject.Domain.User.Repositories;
// using Shared.General;
//
// namespace TimeProject.Api.Modules.User.UseCases;
//
// public class SetIsVerifiedUserUseCase(IUserRepository repository) : ISetIsVerifiedUserUseCase
// {
//     public async Task<Result<bool>> Handle(int id, bool isVerified)
//     {
//         var result = new Result<bool>();
//         var user = await repository.FindById(id);
//
//         if (user == null)
//             return result.SetError(UserMessageErrors.NotFound);
//
//         user.IsVerified = isVerified;
//         await repository.Update(user);
//
//         result.Data = user.IsVerified;
//
//         return result;
//     }
// }
//
