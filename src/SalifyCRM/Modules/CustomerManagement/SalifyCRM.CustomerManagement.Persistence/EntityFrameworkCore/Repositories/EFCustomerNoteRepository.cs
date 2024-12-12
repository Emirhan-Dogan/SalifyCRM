﻿using Core.Persistence.EntityFrameworkCore;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Repositories
{
    public class EFCustomerNoteRepository : EFEntityBaseRepository<CustomerNote, SalifyDbContext>, ICustomerNoteRepository
    {
        public EFCustomerNoteRepository(SalifyDbContext salifyDbContext) : base(salifyDbContext)
        {

        }
    }
}