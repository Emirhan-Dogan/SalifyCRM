using Core.Persistence;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.RepositoryInterfaces
{
    public interface IGroupRepository : IEntityBaseRepository<Group>
    {
    }
}
