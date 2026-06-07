using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.Dtos.Records;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.UseCases.Periods;
using TimeProject.Domain.UseCases.Records;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Application.UseCases.Records;

public class CreateRecordUseCase(
    IUnitOfWork unitOfWork,
    IRecordMapDataUtil mapDataUtil,
    ICreatePeriodByListUseCase createPeriodByListUseCase)
    : ICreateRecordUseCase
{
    public ICustomResult<IRecordOutDto> Handle(ICreateRecordData data, IList<IPeriodData>? periods, int userId)
    {
        var result = new CustomResult<IRecordOutDto>();

        if (data.CategoryId != null)
        {
            var category = unitOfWork.CategoryRepository.FindById((int)data.CategoryId, userId);
            if (category == null)
            {
                return result.SetError(RecordMessageErrors.CategoryNotFound);
            }
        }

        if (string.IsNullOrEmpty(data.Code) == false)
        {
            var trByCode = unitOfWork.RecordRepository.FindByCode(data.Code!, userId);
            if (trByCode != null) return result.SetError(RecordMessageErrors.CodeAlreadyInUse);
        }

        var transaction = unitOfWork.Context.Database.BeginTransaction();
        var record = unitOfWork.RecordRepository
            .Create(new Record
                {
                    UserId = userId,
                    CategoryId = data.CategoryId,
                    Name = data.Name,
                    Description = data.Description,
                    Code = string.IsNullOrEmpty(data.Code) == false ? data.Code! : Guid.NewGuid().ToString(),
                    ExternalLink = data.ExternalLink
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
                            Type = data.SessionType,
                            From = data.SessionFrom
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