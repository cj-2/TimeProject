using AutoMapper;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.UseCases.Users.Dtos;
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