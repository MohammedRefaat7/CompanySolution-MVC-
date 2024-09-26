using Company.BLL.Interfaces;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _dbcontext;
        public GenericRepository(CompanyDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public int Add(T item)
        {
            _dbcontext.Add(item);
            return _dbcontext.SaveChanges();
        }

        public int Delete(T item)
        {
            _dbcontext.Remove(item);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            { return (IEnumerable<T>) _dbcontext.Employees.Include(E => E.Department).ToList(); }
            return _dbcontext.Set<T>().ToList();
        }
        public T GetById(int Id)
        => _dbcontext.Set<T>().Find(Id);

        public int Update(T item)
        {
            _dbcontext.Update(item);
            return _dbcontext.SaveChanges();
        }
    }
}
