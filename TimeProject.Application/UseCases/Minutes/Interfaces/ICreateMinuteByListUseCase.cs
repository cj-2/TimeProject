using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Minutes.Dtos;

namespace TimeProject.Application.UseCases.Minutes.Interfaces;

public interface ICreateMinuteByListUseCase
{
    ICustomResult<List<CreateMinuteByListOutDto>> Handle(CreateMinuteByListDto dto, int userId);
}