using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.AuthModule.ActivationFeature.Command
{
    public class ActivationCommand : IRequest<ActionStatusDto>
    {
        public required string UserId { get; set; }
        public required string AdminId { get; set; }
        public bool Activate { get; set; }
    }
}
