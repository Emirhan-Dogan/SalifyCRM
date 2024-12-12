
using Core.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Repositories
{
    public class EFUserRepository : EFEntityBaseRepository<User, SalifyDbContext>, IUserRepository
    {
        public EFUserRepository(SalifyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly SalifyDbContext _dbContext;

        public List<OperationClaim> GetClaimsForUser(int userId)
        {
            var operationClaimForUser =
                from users in _dbContext.Users
                join userOperationClaims in _dbContext.UserOperationClaims
                on users.Id equals userOperationClaims.UserId
                join operationClaims in _dbContext.OperationClaims
                on userOperationClaims.OperationClaimId equals operationClaims.Id
                where operationClaims.IsDeleted == false && users.Id == userId
                select new OperationClaim
                {
                    Id = operationClaims.Id,
                    Name = operationClaims.Name,
                    Descriptions = operationClaims.Descriptions
                };

            var operationClaimForGroup =
                from users in _dbContext.Users
                join userGroups in _dbContext.UserGroups
                on users.Id equals userGroups.UserId
                join groups in _dbContext.Groups
                on userGroups.GroupId equals groups.Id
                join groupClaim in _dbContext.GroupClaims
                on userGroups.GroupId equals groupClaim.GroupId
                join operationClaims in _dbContext.OperationClaims
                on groupClaim.OperationClaimId equals operationClaims.Id
                where operationClaims.IsDeleted == false && users.Id == userId
                select new OperationClaim
                {
                    Id = operationClaims.Id,
                    Name = operationClaims.Name,
                    Descriptions = operationClaims.Descriptions
                };

            return operationClaimForUser.Concat(operationClaimForGroup).ToList();
        }




        public bool IsEmailAvailable(string email)
        {
            return (_dbContext.Set<User>().Where(O => O.Email == email).ToList() == null
                ? true : false);
        }
    }
}
