using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using SalifyCRM.IdentityServer.Domain.Entities;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Repositories
{
    public class EFUserOperationClaimRepository : EFEntityBaseRepository<UserOperationClaim, SalifyDbContext>, IUserOperationClaimRepository
    {
        private SalifyDbContext _salifyDbContext;

        public EFUserOperationClaimRepository(SalifyDbContext dbContext) : base(dbContext)
        {
            _salifyDbContext = dbContext;
        }

        public IList<UserResponse> GetUsersForOperationClaimId(int operationClaimId)
        {
            var result = from operationClaims in _salifyDbContext.OperationClaims
                         join userOperationClaims in _salifyDbContext.UserOperationClaims
                         on operationClaims.Id equals userOperationClaims.OperationClaimId
                         join users_ in _salifyDbContext.Users
                         on userOperationClaims.UserId equals users_.Id
                         where operationClaims.Id == operationClaimId && users_.IsDeleted == false
                         select new UserResponse
                         {
                             Id = users_.Id,
                             BirthDate = users_.BirthDate,
                             CitizenId = users_.CitizenId,
                             Email = users_.Email,
                             FirstName = users_.FirstName,
                             LastName = users_.LastName,
                             Gender = users_.Gender,
                             LastLoginDate = users_.LastLoginDate,
                             Notes = users_.Notes,
                             PhoneNumber = users_.PhoneNumber,
                             ProfileImage = users_.ProfileImage,
                             Status = users_.Status
                         };
            return result.ToList();
        }
    }
}
