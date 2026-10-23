using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, CreatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdateByAdminPasswordDto dto, bool saveChanges = true);
}