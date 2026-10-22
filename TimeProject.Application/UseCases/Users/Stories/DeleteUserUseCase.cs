using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

public class DeleteUserUseCase(IUnitOfWork unitOfWork) : IDeleteUserUseCase
{
    public ICustomResult<bool> Handle(int id)
    {
        unitOfWork.UserRepository.Delete(id);
        unitOfWork.SaveChanges();
        return new CustomResult<bool> { Data = true };
    }
}