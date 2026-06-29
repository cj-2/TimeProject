using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class UpdateUserRoleUseCase(IUnitOfWork unitOfWork, IUserMapDataUtil mapper) : IUpdateUserRoleUseCase
{
    public ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto)
    {
        var result = new CustomResult<UserOutDto>();
        var user = unitOfWork.UserRepository.FindById(id);

        if (user == null)
            return result.SetError(UserMessageErrors.NotFound);

        if (Enum.TryParse(typeof(UserRoleType), dto.Role, out var userRole) == false)
            return result.SetError(UserMessageErrors.RoleNotFound);

        user.UserRole = (UserRoleType)userRole;

        var entity = unitOfWork.UserRepository.Update(user);
        unitOfWork.SaveChanges();
        result.Data = mapper.Handle(entity);
        return result;
    }
}