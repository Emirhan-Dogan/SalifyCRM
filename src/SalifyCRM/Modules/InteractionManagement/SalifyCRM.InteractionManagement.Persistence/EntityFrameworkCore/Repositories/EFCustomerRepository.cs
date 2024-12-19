using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Domain.Entities;
using SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFCustomerRepository : EFEntityBaseRepository<Customer, SalifyDbContext>, ICustomerRepository
    {
        public EFCustomerRepository(SalifyDbContext salifyDbContext) : base(salifyDbContext)
        {

        }
    }
}
