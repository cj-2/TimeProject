using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, CreatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdateByAdminPasswordDto dto, bool saveChanges = true);
}