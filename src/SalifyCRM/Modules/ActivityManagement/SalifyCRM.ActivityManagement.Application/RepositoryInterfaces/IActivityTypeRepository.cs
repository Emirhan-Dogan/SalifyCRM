using Core.Persistence;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.RepositoryInterfaces
{
    public interface IActivityTypeRepository : IEntityBaseRepository<ActivityType>
    {
    }
}
