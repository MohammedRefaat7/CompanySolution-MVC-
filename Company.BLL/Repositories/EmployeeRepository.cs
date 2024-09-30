﻿using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository 
    {
        private readonly CompanyDbContext _dbContext;
        public EmployeeRepository(CompanyDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;    
        }

        

        public IQueryable<Employee> GetEmployeesByName(string Name)
        =>  _dbContext.Employees.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
        
    }
}
