using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils.Interfaces;

public interface IUserMapDataUtil
{
    UserOutDto Handle(User entity);
    IEnumerable<UserOutDto> Handle(IEnumerable<User> entity);
}