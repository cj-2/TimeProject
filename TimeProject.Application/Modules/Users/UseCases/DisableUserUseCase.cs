using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class DisableUserUseCase(IUnitOfWork unitOfWork) : IDisableUserUseCase
{
    public ICustomResult<bool> Handle(int id, DisableUserDto dto)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.User.FindById(id);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        user.IsActive = dto.IsActive;
        unitOfWork.User.Update(user);
        unitOfWork.SaveChanges();

        result.Data = user.IsActive;
        return result;
    }
}