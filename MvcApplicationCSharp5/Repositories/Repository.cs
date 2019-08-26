using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MvcApplicationCSharp5.Models;

namespace MvcApplicationCSharp5.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ApplicationDbContext Context;
        protected DbSet<T> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return DbSet.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }
    }
}
