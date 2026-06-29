using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, ICreatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, IUpdatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, IUpdateByAdminPasswordDto dto, bool saveChanges = true);
}