using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Repositories
{
    public class EFGroupClaimRepository : EFEntityBaseRepository<GroupClaim, SalifyDbContext>, IGroupClaimRepository
    {
        private SalifyDbContext _salifyDbContext;

        public EFGroupClaimRepository(SalifyDbContext dbContext) : base(dbContext)
        {
            _salifyDbContext = dbContext;
        }

        public void AddBulkOperationClaimsInGroup(List<int> operationClaimsId, int groupId, int createdUserId)
        {
            DateTime createdDate = DateTime.UtcNow;

            foreach (var operationClaim in operationClaimsId)
            {
                _salifyDbContext.GroupClaims.Add(new GroupClaim()
                {
                    GroupId = groupId,
                    OperationClaimId = operationClaim,
                    CreatedDate = createdDate,
                    IsDeleted = false,
                    LastUpdatedDate = createdDate,
                    Status = true,
                    CreatedUserId = createdUserId,
                    LastUpdatedUserId = createdUserId,
                });
            }
            _salifyDbContext.SaveChanges();
        }

        public IList<OperationClaimResponse> GetOperationClaimForGroupId(int groupId)
        {
            var result = from groups in _salifyDbContext.Groups
                         join groupClaims in _salifyDbContext.GroupClaims
                         on groups.Id equals groupClaims.GroupId
                         join operationClaims in _salifyDbContext.OperationClaims
                         on groupClaims.OperationClaimId equals operationClaims.Id
                         where groups.Id == groupId && groups.IsDeleted == false
                         select new OperationClaimResponse
                         {
                             Id = operationClaims.Id,
                             Descriptions = operationClaims.Descriptions,
                             Name = operationClaims.Name
                         };

            return result.ToList();
        }

        public IList<GroupResponse> GetGroupForOperationClaimId(int id)
        {
            var result = from operationClaims in _salifyDbContext.OperationClaims
                         join groupClaims in _salifyDbContext.GroupClaims
                         on operationClaims.Id equals groupClaims.GroupId
                         join groups in _salifyDbContext.Groups
                         on groupClaims.GroupId equals groups.Id
                         where operationClaims.Id == id && groups.IsDeleted == false
                         select new GroupResponse
                         {
                             Id = groups.Id,
                             Name = groups.Name,
                             Descriptions = groups.Descriptions,
                             Status = groups.Status
                         };

            return result.ToList();
        }
    }
}
