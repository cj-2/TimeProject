using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class CreateOrUpdateUserPasswordByEmailUseCase(IUnitOfWork unitOfWork)
    : ICreateOrUpdateUserPasswordByEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string password)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.UserRepository.FindByEmail(email);

        if (user == null)
            return result.SetError(UserMessageErrors.NotFound);

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