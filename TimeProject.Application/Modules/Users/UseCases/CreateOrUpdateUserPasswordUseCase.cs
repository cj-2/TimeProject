using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

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
        var entity = unitOfWork.UserPassword.FindByUserId(userId);

        if (entity != null && (!string.IsNullOrEmpty(oldPassword) || skipOldPasswordCompare))
        {
            if (skipOldPasswordCompare == false && BCrypt.Net.BCrypt.Verify(oldPassword, entity.Password) == false)
                return result.SetError(UserErrors.DifferentPassword);

            entity.Password = BCrypt.Net.BCrypt.HashPassword(password);
            unitOfWork.UserPassword.Update(entity);
        }
        else
        {
            unitOfWork.UserPassword.Create(new UserPassword
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