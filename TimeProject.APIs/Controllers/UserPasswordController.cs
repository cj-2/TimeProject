using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.UseCases.Users;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("user/password")]
public class UserPasswordController(
    // IUpdateUserUseCase updateUserUseCase,
    ICreateOrUpdateUserPasswordUseCase createOrUpdateUserPasswordUseCase,
    IRecoveryPasswordUseCase recoveryPasswordUseCase
) : CustomController
{
    [HttpPut("{id:int}")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<bool> Update([FromRoute] int id, [FromBody] UpdatePasswordDto dto)
    {
        return HasAuthorization(id)
            ? HandleResponse(createOrUpdateUserPasswordUseCase.Handle(id, dto))
            : Forbid();
    }

    [HttpPut("admin/{id:int}")]
    [Authorize(Policy = "IsAdmin")]
    public ActionResult<bool> UpdateByAdmin(
        [FromRoute] int id,
        [FromBody] UpdateByAdminPasswordDto dto
    )
    {
        return HandleResponse(createOrUpdateUserPasswordUseCase.Handle(id, dto));
    }

    [HttpPost]
    [Route("recovery")]
    public ActionResult<bool> Recovery([FromBody] RecoveryPasswordDto dto)
    {
        return HandleResponse(recoveryPasswordUseCase.Handle(dto));
    }

    // [HttpPost, Route("copy/to/new/table")]
    // [Authorize(Policy = "IsAdmin")]
    // public ActionResult<bool> CopyToNewTable()
    // {
    //     var now = DateTime.Now.ToUniversalTime();
    //     
    //     var users =db.Users
    //         .Select(e => new UserPasswordEntity { UserId = e.Id, Password = e.Password!, CreatedAt = now, UpdatedAt = now})
    //         .ToListAsync();
    //
    //     var migratedPasswords =db.UserPasswords.Select(e => e.UserId).ToListAsync();
    //
    //     users.RemoveAll(e => migratedPasswords.Contains(e.UserId));
    //
    //db.UserPasswords.AddRangeAsync(users);
    //db.SaveChangesAsync();
    //     return Ok();
    // }
}