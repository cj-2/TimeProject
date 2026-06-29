using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface ICreateConfirmCodeUseCase
{
    ICustomResult<IConfirmCode> Handle(int userId, ConfirmCodeType type);
}