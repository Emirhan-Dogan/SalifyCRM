using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.ProductManagement.Application.RepositoryInterfaces;
using SalifyCRM.ProductManagement.Domain.Entities;
using SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFColorRepository : EFEntityBaseRepository<Color, SalifyDbContext>, IColorRepository
    {
        public EFColorRepository(SalifyDbContext context) : base(context)
        {

        }
    }
}
