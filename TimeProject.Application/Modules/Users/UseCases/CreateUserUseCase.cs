using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Modules.Auth.Interfaces;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Domain;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class CreateUserUseCase(
    IUnitOfWork unitOfWork,
    IUserMapDataUtil mapper,
    IJwtHandler jwtHandler,
    ICreateOrUpdateUserPasswordUseCase createUserPasswordUseCase
) : ICreateUserUseCase
{
    public ICustomResult<CreateUserOutDto> Handle(CreateUserDto dto)
    {
        var result = new CustomResult<CreateUserOutDto>();
        var emailAvailable = unitOfWork.User.EmailIsAvailable(dto.Email);

        if (emailAvailable == false) return result.SetError(UserErrors.EmailAlreadyInUse);

        User user;

        try
        {
            user = new User { Name = dto.Name, Email = dto.Email, Timezone = dto.Timezone };
        }
        catch (DomainException ex)
        {
            return result.SetError(ex.Message);
        }

        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.User.Create(user);
            unitOfWork.SaveChanges();

            createUserPasswordUseCase.Handle(user.UserId, new CreatePasswordDto { Password = dto.Password }, false);
            unitOfWork.SaveChanges();
        }
        catch
        {
            unitOfWork.RollbackTransaction();
            return result.SetError(GeneralErrors.TransactionError);
        }

        unitOfWork.CommitTransaction();

        return result.SetData(new CreateUserOutDto(jwtHandler.Generate(user)));
    }
}