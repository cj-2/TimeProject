using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Records;

public class CreateRecordUseCase(
    IUnitOfWork unitOfWork,
    IRecordMapDataUtil mapDataUtil,
    ICreatePeriodByListUseCase createPeriodByListUseCase)
    : ICreateRecordUseCase
{
    public ICustomResult<RecordOutDto> Handle(CreateRecordDto dto, IList<PeriodDto>? periods, int userId)
    {
        var result = new CustomResult<RecordOutDto>();

        if (dto.CategoryId != null)
        {
            var category = unitOfWork.CategoryRepository.FindById((int)dto.CategoryId, userId);
            if (category == null)
            {
                return result.SetError(RecordMessageErrors.CategoryNotFound);
            }
        }

        if (string.IsNullOrEmpty(dto.Code) == false)
        {
            var trByCode = unitOfWork.RecordRepository.FindByCode(dto.Code!, userId);
            if (trByCode != null) return result.SetError(RecordMessageErrors.CodeAlreadyInUse);
        }

        var transaction = unitOfWork.Context.Database.BeginTransaction();
        var record = unitOfWork.RecordRepository
            .Create(new Record
                {
                    UserId = userId,
                    CategoryId = dto.CategoryId,
                    Name = dto.Name,
                    Description = dto.Description,
                    Code = string.IsNullOrEmpty(dto.Code) == false ? dto.Code! : Guid.NewGuid().ToString(),
                    ExternalLink = dto.ExternalLink
                }
            );

        unitOfWork.SaveChanges();

        try
        {
            if (periods != null)
            {
                var periodsResult = createPeriodByListUseCase
                    .Handle(
                        new PeriodListDto
                        {
                            Periods = periods,
                            Type = dto.SessionType,
                            From = dto.SessionFrom
                        },
                        record.RecordId, userId);

                if (periodsResult.HasError)
                    throw new Exception(periodsResult.Message);
            }
        }
        catch (Exception error)
        {
            transaction.Rollback();
            return result.SetError(error.Message);
        }

        transaction.Commit();
        return result.SetData(mapDataUtil.Handle(record));
    }
}