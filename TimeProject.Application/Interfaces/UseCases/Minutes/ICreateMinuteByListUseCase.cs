using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Minutes;

public interface ICreateMinuteByListUseCase
{
    ICustomResult<IList<IMinute>> Handle(CreateMinuteListDto dto, int userId);
}