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
        public void Add(T item)
        {
            _dbcontext.Add(item);
        }

        public void Delete(T item)
        {
            _dbcontext.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            { return (IEnumerable<T>)_dbcontext.Employees.Include(E => E.Department).ToList(); }
            return _dbcontext.Set<T>().ToList();
        }
        public T GetById(int Id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (T)(object)_dbcontext.Employees.Include(e => e.Department).Where(e => e.Id == Id).FirstOrDefault();
            }
            return _dbcontext.Set<T>().Find(Id);
        }
        public void Update(T item)
        {
            _dbcontext.Update(item);
        }   
        
    }
}
