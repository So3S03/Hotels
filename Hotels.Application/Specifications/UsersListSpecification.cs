using Hotels.Application.AuthModule.UsersGridFeature.Query;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Identity;

namespace Hotels.Application.Specifications
{
    internal class UsersListSpecification : BaseSpecification<AppUser>
    {
        public UsersListSpecification(UsersQuery parameters): base(string.IsNullOrEmpty(parameters.Name) ? null : u => u.FullName.ToLower().Contains(parameters.Name.ToLower()))
        {
            if(parameters.PageNum > 0 && parameters.PageSize > 0)
            {
                Pagination(parameters.PageNum, parameters.PageSize);
            }
        }
    }
}
