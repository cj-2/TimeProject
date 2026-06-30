using AutoMapper;
using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class UserMapDataUtil(IMapper mapper) : IUserMapDataUtil
{
    public UserOutDto Handle(User entity)
    {
        return mapper.Map<User, UserOutDto>(entity);
    }

    public IEnumerable<UserOutDto> Handle(IEnumerable<User> entity)
    {
        return mapper.Map<IEnumerable<User>, IEnumerable<UserOutDto>>(entity);
    }
}