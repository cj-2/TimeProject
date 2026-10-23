using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class CreateOrUpdateUserPasswordByEmailUseCase(IUnitOfWork unitOfWork)
    : ICreateOrUpdateUserPasswordByEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string password)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.User.FindByEmail(email);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        var userPassword = unitOfWork.UserPassword.FindByUserId(user.UserId);

        if (userPassword != null)
        {
            userPassword.Password = BCrypt.Net.BCrypt.HashPassword(password);
            unitOfWork.UserPassword.Update(userPassword);
        }
        else
        {
            unitOfWork.UserPassword
                .Create(new UserPassword { Password = BCrypt.Net.BCrypt.HashPassword(password) });
        }

        unitOfWork.SaveChanges();

        result.Data = true;
        return result;
    }
}