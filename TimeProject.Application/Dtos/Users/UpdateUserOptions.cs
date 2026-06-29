using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Dtos.Users;

public class UpdateUserOptions : IUpdateUserOptions
{
    public bool UpdateFromAdmin { get; set; }
}