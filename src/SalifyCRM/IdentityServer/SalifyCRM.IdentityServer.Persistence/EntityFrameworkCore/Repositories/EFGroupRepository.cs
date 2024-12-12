using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Repositories
{
    public class EFGroupRepository : EFEntityBaseRepository<Group, SalifyDbContext>, IGroupRepository
    {
        public EFGroupRepository(SalifyDbContext dbContext) : base(dbContext)
        {

        }
    }
}
