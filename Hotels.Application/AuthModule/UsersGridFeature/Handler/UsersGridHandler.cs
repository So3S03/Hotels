using Hotels.Application.AuthModule.UsersGridFeature.Query;
using Hotels.Application.Specifications;
using Hotels.Domain.Entities.Identity;
using Hotels.Domain.SpecificationPattern;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.AuthModule;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.AuthModule.UsersGridFeature.Handler
{
    public class UsersGridHandler(UserManager<AppUser> _userManager, IMapper _mapper) : IRequestHandler<UsersQuery, GridsToReturnDto<UserToReturnDto>>
    {
        public async Task<GridsToReturnDto<UserToReturnDto>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            var Spec = new UsersListSpecification(request);
            var List = await SpecificationEvaluator.GenerateQuery(_userManager.Users, Spec).ToListAsync();
            var total = await SpecificationEvaluator.GenerateQuery(_userManager.Users, Spec).CountAsync();
            var mappedUsers = _mapper.Map<ICollection<UserToReturnDto>>(List);
            return new GridsToReturnDto<UserToReturnDto>
            {
                Data = mappedUsers,
                Total = total
            };
        }
    }
}
