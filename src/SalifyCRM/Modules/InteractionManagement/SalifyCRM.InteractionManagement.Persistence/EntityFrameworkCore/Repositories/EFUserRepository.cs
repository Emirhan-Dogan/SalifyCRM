using Core.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Domain.Entities;
using SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFUserRepository : EFEntityBaseRepository<User, SalifyDbContext>, IUserRepository
    {
        public EFUserRepository(SalifyDbContext dbContext) : base(dbContext)
        {

        }

    }
}
