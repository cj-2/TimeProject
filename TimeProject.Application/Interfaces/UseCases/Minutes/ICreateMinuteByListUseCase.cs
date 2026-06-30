using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Minutes;

public interface ICreateMinuteByListUseCase
{
    ICustomResult<IList<Minute>> Handle(CreateMinuteListDto dto, int userId);
}