using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Minutes;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Minutes;

public class CreateMinuteByListUseCase(
    IMinuteRepository minuteRepository,
    IRecordRepository recordRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository,
    ISyncRecordResumeUseCase syncRecordResumeUseCase
) : ICreateMinuteByListUseCase
{
    public ICustomResult<IList<IMinute>> Handle(CreateMinuteListDto dto, int userId)
    {
        var result = new CustomResult<IList<IMinute>>();
        IList<IMinute> list = [];

        if (dto.RecordId == null && dto.CategoryId == null)
        {
            return result.SetError(MinuteMessageErrors.Required);
        }
        
        var user = userRepository.FindById(userId);
        if (user is null) return result.SetError(UserMessageErrors.NotFound);

        if (dto.RecordId != null)
        {
            var record = recordRepository.FindById((int)dto.RecordId, userId);
            if (record is null) return result.SetError(RecordMessageErrors.NotFound);

            foreach (var minutes in dto.Minutes)
                list.Add(new Minute
                {
                    UserId = userId,
                    RecordId = dto.RecordId,
                    Total = minutes,
                    Date = dto.Date
                });
        }
        else if (dto.CategoryId != null)
        {
            var category = categoryRepository.FindById((int)dto.CategoryId, userId);
            if (category is null) return result.SetError(CategoryMessageErrors.NotFound);

            foreach (var minutes in dto.Minutes)
                list.Add(new Minute
                {
                    UserId = userId,
                    CategoryId = dto.CategoryId,
                    Total = minutes,
                    Date = dto.Date
                });
        }

        var data = minuteRepository.CreateByList(list);
        if (dto.RecordId != null)
        {
            syncRecordResumeUseCase.Handle((int)dto.RecordId);
        }

        return result.SetData(data);
    }
}