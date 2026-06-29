using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class CreateOrUpdateUserPasswordUseCase(IUnitOfWork unitOfWork)
    : ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, CreatePasswordDto dto, bool saveChanges = true)
    {
        return _handle(userId, dto.Password, saveChanges: saveChanges);
    }

    public ICustomResult<bool> Handle(int userId, UpdatePasswordDto dto, bool saveChanges = true)
    {
        return _handle(userId, dto.Password, dto.OldPassword, saveChanges: saveChanges);
    }

    public ICustomResult<bool> Handle(int userId, UpdateByAdminPasswordDto dto, bool saveChanges = true)
    {
        return _handle(userId, dto.Password, "", true, saveChanges: saveChanges);
    }

    private ICustomResult<bool> _handle(
        int userId,
        string password,
        string oldPassword = "",
        bool skipOldPasswordCompare = false,
        bool saveChanges = true
    )
    {
        var result = new CustomResult<bool>();
        var entity = unitOfWork.UserPasswordRepository.FindByUserId(userId);

        if (entity != null && (!string.IsNullOrEmpty(oldPassword) || skipOldPasswordCompare))
        {
            if (skipOldPasswordCompare == false && BCrypt.Net.BCrypt.Verify(oldPassword, entity.Password) == false)
                return result.SetError(UserMessageErrors.DifferentPassword);

            entity.Password = BCrypt.Net.BCrypt.HashPassword(password);
            unitOfWork.UserPasswordRepository.Update(entity);
        }
        else
        {
            unitOfWork.UserPasswordRepository.Create(new UserPassword
            {
                UserId = userId,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                IsActive = true
            });
        }

        if (saveChanges)
        {
            unitOfWork.SaveChanges();
        }
        
        result.Data = true;
        return result;
    }
}