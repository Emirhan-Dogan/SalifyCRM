
using Core.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFUserRepository : EFEntityBaseRepository<User, SalifyDbContext>, IUserRepository
    {
        public EFUserRepository(SalifyDbContext dbContext) : base(dbContext)
        {

        }

    }
}
