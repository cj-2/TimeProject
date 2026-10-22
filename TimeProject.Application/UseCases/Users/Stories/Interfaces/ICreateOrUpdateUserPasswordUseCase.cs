using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface ICreateOrUpdateUserPasswordUseCase
{
    public ICustomResult<bool> Handle(int userId, CreatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdatePasswordDto dto, bool saveChanges = true);
    public ICustomResult<bool> Handle(int userId, UpdateByAdminPasswordDto dto, bool saveChanges = true);
}