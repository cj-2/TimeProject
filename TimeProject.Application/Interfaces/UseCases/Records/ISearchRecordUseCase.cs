using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISearchRecordUseCase
{
    ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId);
}