using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Users;

public class GetUserPasswordByEmailOutDto
{
    public IUserPassword UserPassword { get; set; } = null!;
    public IUser User { get; set; } = null!;
}