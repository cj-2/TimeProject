using TimeProject.Application.Modules.Minutes.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Minutes.Interfaces;

public interface ICreateMinuteByListUseCase
{
    ICustomResult<List<CreateMinuteByListOutDto>> Handle(CreateMinuteByListDto dto, int userId);
}