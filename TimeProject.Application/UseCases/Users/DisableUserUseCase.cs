using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.UseCases.Users;
using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class DisableUserUseCase(IUnitOfWork unitOfWork) : IDisableUserUseCase
{
    public ICustomResult<bool> Handle(int id, IDisableUserDto dto)
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