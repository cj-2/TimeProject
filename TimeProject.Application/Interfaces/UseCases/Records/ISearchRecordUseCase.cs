using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.ObjectValues;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISearchRecordUseCase
{
    ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId);
}