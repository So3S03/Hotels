using Hotels.Application._Common;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.AuthModule;
using MediatR;

namespace Hotels.Application.AuthModule.UsersGridFeature.Query
{
    public class UsersQuery : CommonGridQuery<UserToReturnDto>
    {
        public string? Name { get; set; }
    }
}
