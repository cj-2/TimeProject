using TimeProject.Domain.Dtos.Auths;
using TimeProject.Domain.Dtos.Users;

namespace TimeProject.Domain.ObjectValues;

public interface ICreateUserResult
{
    IUserOutDto User { get; set; }
}