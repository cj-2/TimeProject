using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class UpdateUserRoleUseCase(IUnitOfWork unitOfWork) : IUpdateUserRoleUseCase
{
    public ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto)
    {
        var result = new CustomResult<UserOutDto>();
        var user = unitOfWork.User.FindById(id);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        if (Enum.TryParse(typeof(UserRoleType), dto.Role, out var userRole) == false)
            return result.SetError(UserErrors.RoleNotFound);

        user.UserRole = (UserRoleType)userRole;

        var entity = unitOfWork.User.Update(user);
        unitOfWork.SaveChanges();

        return result.SetData(UserOutDto.Map(entity));
    }
}