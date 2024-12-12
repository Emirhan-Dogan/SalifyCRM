using Core.Persistence;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.RepositoryInterfaces
{
    public interface IUserGroupRepository : IEntityBaseRepository<UserGroup>
    {
        void AddBulkUsersInGroup(List<int> usersId, int groupId, int createdUserId);
        IList<UserResponse> GetUsersForGroupId(int id);
    }
}
