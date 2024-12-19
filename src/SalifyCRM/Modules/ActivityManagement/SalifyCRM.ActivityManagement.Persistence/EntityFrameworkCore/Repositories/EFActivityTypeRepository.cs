using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.ActivityManagement.Application.RepositoryInterfaces;
using SalifyCRM.ActivityManagement.Domain.Entities;
using SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFActivityTypeRepository : EFEntityBaseRepository<ActivityType, SalifyDbContext>, IActivityTypeRepository
    {
        public EFActivityTypeRepository(SalifyDbContext context) : base(context)
        {
            
        }
    }
}
