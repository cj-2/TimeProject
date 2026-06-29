using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Minutes;

namespace TimeProject.Application.Interfaces.UseCases.Minutes;

public interface ICreateMinuteByListUseCase
{
    ICustomResult<IList<IMinute>> Handle(CreateMinuteListDto dto, int userId);
}