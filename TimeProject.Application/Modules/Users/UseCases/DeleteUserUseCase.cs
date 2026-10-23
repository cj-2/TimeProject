using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class DeleteUserUseCase(IUnitOfWork unitOfWork) : IDeleteUserUseCase
{
    public ICustomResult<bool> Handle(int id)
    {
        unitOfWork.User.Delete(id);
        unitOfWork.SaveChanges();
        return new CustomResult<bool> { Data = true };
    }
}