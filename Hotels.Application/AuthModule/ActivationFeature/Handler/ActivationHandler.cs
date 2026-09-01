using Hotels.Application.AuthModule.ActivationFeature.Command;
using Hotels.Domain.Entities.Identity;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Hotels.Application.AuthModule.ActivationFeature.Handler
{
    public class ActivationHandler(UserManager<AppUser> _userManager) : IRequestHandler<ActivationCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(ActivationCommand request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { UserId: null or ""} => throw new BadRequest400Exception("Invalid UserId"),
                { AdminId: null or ""} => throw new BadRequest400Exception("Invalid AdminId"),
                { AdminId: var adminId, UserId: var userId } when adminId == userId => throw new BadRequest400Exception("You can't activate/deactivate your own account"),
                _ => request
            };
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null) throw new NotFound404Exception("User Not Found!");
            if(user.isActive == request.Activate) throw new Conflict409Exception($"User is already {(request.Activate ? "activated" : "deactivated")}");
            user.isActive = request.Activate;
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded) throw new Exception("Something Went Wrong");
            var Obj = new ActionStatusDto
            {
                Succeeded = true,
                Message = $"User {(request.Activate ? "Activated" : "Deactivated")} Successfully"
            };
            return Obj;
        }
    }
}
