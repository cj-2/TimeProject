using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

public class CreateOrUpdateUserPasswordByEmailUseCase(IUnitOfWork unitOfWork)
    : ICreateOrUpdateUserPasswordByEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string password)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.UserRepository.FindByEmail(email);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        var userPassword = unitOfWork.UserPasswordRepository.FindByUserId(user.UserId);

        if (userPassword != null)
        {
            userPassword.Password = BCrypt.Net.BCrypt.HashPassword(password);
            unitOfWork.UserPasswordRepository.Update(userPassword);
        }
        else
        {
            unitOfWork.UserPasswordRepository
                .Create(new UserPassword { Password = BCrypt.Net.BCrypt.HashPassword(password) });
        }

        unitOfWork.SaveChanges();

        result.Data = true;
        return result;
    }
}