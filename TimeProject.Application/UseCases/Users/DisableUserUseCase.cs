using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class DisableUserUseCase(IUnitOfWork unitOfWork) : IDisableUserUseCase
{
    public ICustomResult<bool> Handle(int id, DisableUserDto dto)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.UserRepository.FindById(id);

        if (user == null)
            return result.SetError(UserMessageErrors.NotFound);

        user.IsActive = dto.IsActive;
        unitOfWork.UserRepository.Update(user);
        unitOfWork.SaveChanges();

        result.Data = user.IsActive;
        return result;
    }
}