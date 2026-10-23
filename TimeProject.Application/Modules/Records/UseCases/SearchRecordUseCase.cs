using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.ObjectValues;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Records.UseCases;

public class SearchRecordUseCase(IRecordRepository repository) : ISearchRecordUseCase
{
    public ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId)
    {
        var result = new CustomResult<IList<SearchRecordItem>>();
        return result.SetData(repository.SearchRecord(search, userId));
    }
}