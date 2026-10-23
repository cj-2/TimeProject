using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Modules.Codes.Interfaces;

public interface ICreateConfirmCodeUseCase
{
    ICustomResult<ConfirmCode> Handle(int userId, ConfirmCodeType type);
}