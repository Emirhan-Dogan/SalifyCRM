using Core.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    public class EFUserGroupRepository : EFEntityBaseRepository<UserGroup, SalifyDbContext>, IUserGroupRepository
    {
        private SalifyDbContext _salifyDbContext;
        public EFUserGroupRepository(SalifyDbContext dbContext) : base(dbContext)
        {
            _salifyDbContext = dbContext;
        }

        public void AddBulkUsersInGroup(List<int> usersId, int groupId, int createdUserId)
        {
            DateTime createdDate = DateTime.UtcNow;

            foreach (var userId in usersId)
            {

                var userGroup = new UserGroup()
                {
                    UserId = userId,
                    GroupId = groupId,
                    CreatedDate = createdDate,
                    IsDeleted = false,
                    LastUpdatedDate = createdDate,
                    CreatedUserId = createdUserId,
                    LastUpdatedUserId = createdUserId,
                    Status = true
                };
                _salifyDbContext.UserGroups.Add(userGroup);
            }

            _salifyDbContext.SaveChanges();
        }

        public IList<UserResponse> GetUsersForGroupId(int id)
        {
            var users = from users_ in _salifyDbContext.Users
                        join userGroups in _salifyDbContext.UserGroups
                        on users_.Id equals userGroups.UserId
                        join groups in _salifyDbContext.Groups
                        on userGroups.GroupId equals groups.Id
                        where users_.Id == id && users_.IsDeleted == false
                        select new UserResponse()
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

            return users.ToList();
        }
    }
}
