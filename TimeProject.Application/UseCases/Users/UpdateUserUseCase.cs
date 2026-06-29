using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.ObjectValues;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class UpdateUserUseCase(IUnitOfWork unitOfWork, IUserMapDataUtil mapper) : IUpdateUserUseCase
{
    public ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto)
    {
        return _update(id, dto, null);
    }

    public ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto, IUpdateUserOptions config)
    {
        return _update(id, dto, config);
    }

    private ICustomResult<UserOutDto> _update(int id, UpdateUserDto dto, IUpdateUserOptions? config)
    {
        var result = new CustomResult<UserOutDto>();
        var user = unitOfWork.UserRepository.FindById(id);

        if (user == null) return result.SetError(UserMessageErrors.NotFound);

        if (!string.IsNullOrWhiteSpace(dto.Email) && user.Email != dto.Email && config?.UpdateFromAdmin == true)
        {
            var emailAvailable = unitOfWork.UserRepository.EmailIsAvailable(dto.Email);
            if (emailAvailable == false) return result.SetError(UserMessageErrors.EmailAlreadyInUse);

            user.Email = dto.Email;
        }

        if (!string.IsNullOrWhiteSpace(dto.Name) && user.Name != dto.Name) user.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Timezone)) user.Timezone = dto.Timezone;

        var entity = unitOfWork.UserRepository.Update(user);
        unitOfWork.SaveChanges();

        result.Data = mapper.Handle(entity);
        return result;
    }
}