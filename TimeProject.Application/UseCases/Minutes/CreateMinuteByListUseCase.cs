using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Minutes.Dtos;
using TimeProject.Application.UseCases.Minutes.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Minutes;

public class CreateMinuteByListUseCase(
    IMinuteRepository minuteRepository,
    IRecordRepository recordRepository,
    ISessionRepository sessionRepository,
    IUserRepository userRepository
) : ICreateMinuteByListUseCase
{
    public ICustomResult<List<CreateMinuteByListOutDto>> Handle(CreateMinuteByListDto dto, int userId)
    {
        var result = new CustomResult<List<CreateMinuteByListOutDto>>();
        IList<Minute> list = [];

        var user = userRepository.FindById(userId);
        if (user is null) return result.SetError(UserMessageErrors.NotFound);

        var record = recordRepository.FindById(dto.RecordId, userId);
        if (record is null) return result.SetError(RecordMessageErrors.NotFound);

        var session = sessionRepository
            .Create(new Session
                {
                    RecordId = record.RecordId,
                    UserId = userId,
                    Type = SessionType.Manual,
                    From = "web",
                    Date = dto.Date
                }
            );

        foreach (var minutes in dto.Minutes)
        {
            list.Add(new Minute
            {
                UserId = userId,
                RecordId = dto.RecordId,
                SessionId = session.SessionId,
                Total = minutes,
                Date = dto.Date
            });
        }

        minuteRepository.CreateByList(list);
        return result.SetData(list.Select(CreateMinuteByListOutDto.Map).ToList());
    }
}