using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Records.UseCases;

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
            return result.SetError(RecordErrors.NotFound);

        if (dto.CategoryId != null)
        {
            var category = categoryRepo.FindById((int)dto.CategoryId, userId);
            if (category == null) return result.SetError(RecordErrors.CategoryNotFound);
            record.CategoryId = dto.CategoryId;
        }

        if (string.IsNullOrEmpty(dto.Code))
            return result.SetError(RecordErrors.CodeMustValue);

        if (record.Code != dto.Code)
        {
            var trByCode = repository.FindByCode(dto.Code, userId);
            if (trByCode != null) return result.SetError(RecordErrors.AlreadyInUse);
        }

        record.Code = dto.Code;

        record.Name = dto.Title;
        record.Description = dto.Description;
        record.ExternalLink = dto.ExternalLink;

        syncRecordResumeUseCase.Handle(record.RecordId);

        return result.SetData(mapDataUtil.Handle(repository.Update(record)));
    }
}