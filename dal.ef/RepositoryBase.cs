using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dal
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        [Inject]
        public DbContext DataContext { get; set; }

        private IDbSet<T> dbset
        {
            get
            {
                return DataContext.Set<T>();
            }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> Page(Expression<Func<T, bool>> where, int pageindex, int pagesize, out int total)
        {
            var query = dbset.AsQueryable();
            if (null != where)
                query = query.Where(where);
            total = query.Count();
            return query.OrderByDescending(o=>0).Skip((pageindex - 1) * pagesize).Take(pagesize);
        }
    }
}
