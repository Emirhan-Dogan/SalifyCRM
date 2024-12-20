using Core.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalifyCRM.ActivityManagement.Application.RepositoryInterfaces;
using SalifyCRM.ActivityManagement.Domain.Entities;
using SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFUserRepository : EFEntityBaseRepository<User, SalifyDbContext>, IUserRepository
    {
        public EFUserRepository(SalifyDbContext dbContext) : base(dbContext)
        {

        }

    }
}
