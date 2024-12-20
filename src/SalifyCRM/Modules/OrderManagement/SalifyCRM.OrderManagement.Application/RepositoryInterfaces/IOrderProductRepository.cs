using Core.Persistence;
using SalifyCRM.OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Application.RepositoryInterfaces
{
    public interface IOrderProductRepository : IEntityBaseRepository<OrderProduct>
    {
    }
}
