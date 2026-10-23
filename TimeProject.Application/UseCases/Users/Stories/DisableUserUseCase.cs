using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

public class DisableUserUseCase(IUnitOfWork unitOfWork) : IDisableUserUseCase
{
    public ICustomResult<bool> Handle(int id, DisableUserDto dto)
    {
        var result = new CustomResult<bool>();
        var user = unitOfWork.UserRepository.FindById(id);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        user.IsActive = dto.IsActive;
        unitOfWork.UserRepository.Update(user);
        unitOfWork.SaveChanges();

        result.Data = user.IsActive;
        return result;
    }
}