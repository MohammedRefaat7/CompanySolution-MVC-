using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext _dbcontext;
        public DepartmentRepository(CompanyDbContext dbContext) : base(dbContext)    //CTOR Chaining
        {
            _dbcontext = dbContext;
        }

        public Department GetByName(string name)
            =>_dbcontext.Departments.Where(d => d.Name == name).FirstOrDefault();
        
    }
}
