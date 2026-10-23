using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

public class CreateUserByGhUserUseCase(IUnitOfWork unitOfWork) : ICreateUserByGhUserUseCase
{
    public ICustomResult<User> Handle(CreateUserOAuthDto dto, IEnumerable<EmailGh> emails)
    {
        var result = new CustomResult<User>();

        if (string.IsNullOrEmpty(dto.UserProviderId))
            return result.SetError(UserErrors.OAuthWithoutProviderId);

        var filtredEmails = emails.Where(e => e.Verified).ToList();
        var primaryEmail = filtredEmails.FirstOrDefault(e => e.Primary);

        if (filtredEmails.Count != 0 && primaryEmail != null)
        {
            var userWithPrimaryEmail = unitOfWork.User.FindByEmail(primaryEmail.Email);

            if (userWithPrimaryEmail != null)
            {
                unitOfWork.UserProvider.Create(new UserProvider
                {
                    UserId = userWithPrimaryEmail.UserId,
                    Provider = "github",
                    ExternalId = dto.UserProviderId
                });
                
                unitOfWork.SaveChanges();

                return result.SetData(userWithPrimaryEmail);
            }

            filtredEmails.Remove(primaryEmail);

            foreach (var secondaryEmail in filtredEmails)
            {
                var userWithSecondaryEmail = unitOfWork.User.FindByEmail(secondaryEmail.Email);
                if (userWithSecondaryEmail == null) continue;

                unitOfWork.UserProvider.Create(new UserProvider
                {
                    UserId = userWithSecondaryEmail.UserId,
                    Provider = "github",
                    ExternalId = dto.UserProviderId
                });

                unitOfWork.SaveChanges();

                return result.SetData(userWithSecondaryEmail);
            }
        }

        var userEntity = unitOfWork.User
            .Create(new User
            {
                Name = dto.Name,
                Email = primaryEmail?.Email ?? "",
                Timezone = ""
            });

        unitOfWork.SaveChanges();

        unitOfWork.UserProvider.Create(new UserProvider
        {
            UserId = userEntity.UserId,
            Provider = "github",
            ExternalId = dto.UserProviderId
        });

        unitOfWork.SaveChanges();

        return result.SetData(userEntity);
    }
}