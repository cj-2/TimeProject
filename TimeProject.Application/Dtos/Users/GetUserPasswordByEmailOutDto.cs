using TimeProject.Domain.Entities;

namespace TimeProject.Application.Dtos.Users;

public class GetUserPasswordByEmailOutDto
{
    public UserPassword UserPassword { get; set; } = null!;
    public User User { get; set; } = null!;
}