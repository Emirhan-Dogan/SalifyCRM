﻿using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.ProductManagement.Application.RepositoryInterfaces;
using SalifyCRM.ProductManagement.Domain.Entities;
using SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFProductTagRepository : EFEntityBaseRepository<ProductTag, SalifyDbContext>, IProductTagRepository
    {
        public EFProductTagRepository(SalifyDbContext context) : base(context)
        {

        }
    }
}