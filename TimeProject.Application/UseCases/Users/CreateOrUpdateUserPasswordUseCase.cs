using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.UseCases.Users;
using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class CreateOrUpdateUserPasswordUseCase(IUnitOfWork unitOfWork)
    : ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, ICreatePasswordDto dto, bool saveChanges = true)
    {
        return _handle(userId, dto.Password, saveChanges: saveChanges);
    }

    public ICustomResult<bool> Handle(int userId, IUpdatePasswordDto dto, bool saveChanges = true)
    {
        return _handle(userId, dto.Password, dto.OldPassword, saveChanges: saveChanges);
    }

    public ICustomResult<bool> Handle(int userId, IUpdateByAdminPasswordDto dto, bool saveChanges = true)
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