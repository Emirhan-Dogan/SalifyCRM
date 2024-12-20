using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.OrderManagement.Application.RepositoryInterfaces;
using SalifyCRM.OrderManagement.Domain.Entities;
using SalifyCRM.OrderManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFOrderProductRepository : EFEntityBaseRepository<OrderProduct, SalifyDbContext>, IOrderProductRepository
    {
        public EFOrderProductRepository(SalifyDbContext context) : base(context)
        {

        }
    }
}
