using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Records;

public class UpdateRecordUseCase(
    IRecordRepository repository,
    IRecordMapDataUtil mapDataUtil,
    ICategoryRepository categoryRepo,
    ISyncRecordResumeUseCase syncRecordResumeUseCase
)
    : IUpdateRecordUseCase
{
    public ICustomResult<RecordOutDto> Handle(int id, UpdateRecordDto dto, int userId)
    {
        var result = new CustomResult<RecordOutDto>();

        var record = repository.FindById(id, userId);

        if (record == null)
            return result.SetError(RecordMessageErrors.NotFound);

        if (dto.CategoryId != null)
        {
            var category = categoryRepo.FindById((int)dto.CategoryId, userId);
            if (category == null) return result.SetError(RecordMessageErrors.CategoryNotFound);
            record.CategoryId = dto.CategoryId;
        }

        if (string.IsNullOrEmpty(dto.Code))
            return result.SetError(RecordMessageErrors.CodeMustValue);

        if (record.Code != dto.Code)
        {
            var trByCode = repository.FindByCode(dto.Code, userId);
            if (trByCode != null) return result.SetError(RecordMessageErrors.AlreadyInUse);
        }

        record.Code = dto.Code;

        record.Name = dto.Title;
        record.Description = dto.Description;
        record.ExternalLink = dto.ExternalLink;

        syncRecordResumeUseCase.Handle(record.RecordId);

        return result.SetData(mapDataUtil.Handle(repository.Update(record)));
    }
}