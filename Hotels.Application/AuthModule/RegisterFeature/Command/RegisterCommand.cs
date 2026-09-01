using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.AuthModule;
using MediatR;

namespace Hotels.Application.AuthModule.RegisterFeature.Command
{
    public class RegisterCommand : IRequest<ActionStatusDto>
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
