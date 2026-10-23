using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Modules.Users.Options;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class UpdateUserUseCase(IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto)
    {
        return _update(id, dto, null);
    }

    public ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto, UpdateUserOptions options)
    {
        return _update(id, dto, options);
    }

    private ICustomResult<UserOutDto> _update(int id, UpdateUserDto dto, UpdateUserOptions? options)
    {
        var result = new CustomResult<UserOutDto>();
        var user = unitOfWork.User.FindById(id);

        if (user == null) return result.SetError(UserErrors.NotFound);

        if (!string.IsNullOrWhiteSpace(dto.Email) && user.Email != dto.Email && options?.UpdateFromAdmin == true)
        {
            var emailAvailable = unitOfWork.User.EmailIsAvailable(dto.Email);
            if (emailAvailable == false) return result.SetError(UserErrors.EmailAlreadyInUse);

            user.UpdateEmail(dto.Email);
        }

        if (!string.IsNullOrWhiteSpace(dto.Name) && user.Name != dto.Name) user.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Timezone)) user.UpdateTimeZone(dto.Timezone);

        var entity = unitOfWork.User.Update(user);
        unitOfWork.SaveChanges();

        return result.SetData(UserOutDto.Map(entity));
    }
}