using Core.Persistence;
using SalifyCRM.IdentityServer.Application.Responses.GroupClaims;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.RepositoryInterfaces
{
    public interface IGroupClaimRepository : IEntityBaseRepository<GroupClaim>
    {
        void AddBulkOperationClaimsInGroup(List<int> operationClaimsId, int groupId, int createdUserId);
        IList<OperationClaimResponse> GetOperationClaimForGroupId(int groupId);
        IList<GroupResponse> GetGroupForOperationClaimId(int id);
    }
}
