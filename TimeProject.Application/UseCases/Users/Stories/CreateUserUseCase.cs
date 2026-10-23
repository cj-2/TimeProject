using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Domain;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

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
        var emailAvailable = unitOfWork.UserRepository.EmailIsAvailable(dto.Email);

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

        unitOfWork.Context.Database.BeginTransaction();

        try
        {
            unitOfWork.UserRepository.Create(user);
            unitOfWork.SaveChanges();

            createUserPasswordUseCase.Handle(user.UserId, new CreatePasswordDto { Password = dto.Password }, false);
            unitOfWork.SaveChanges();
        }
        catch
        {
            unitOfWork.Context.Database.RollbackTransaction();
            return result.SetError(GeneralErrors.TransactionError);
        }

        unitOfWork.Context.Database.CommitTransaction();

        result.Data = new CreateUserOutDto
        {
            User = UserOutDto.Map(user),
            Jwt = jwtHandler.Generate(user)
        };

        return result;
    }
}