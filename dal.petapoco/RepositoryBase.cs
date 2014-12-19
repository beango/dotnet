using model;
using Ninject;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dal
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        [ThreadStatic]
        Database DataContext = new NPoco.Database("NorthwindContext");

        public virtual void Add(T entity)
        {
            DataContext.Save<T>(entity);
        }

        public virtual void Update(T entity)
        {
            DataContext.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            DataContext.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            foreach (var item in GetMany(where))
            {
                DataContext.Delete(item);
            }
        }

        public virtual T GetById(long id)
        {
            return DataContext.SingleOrDefaultById<T>(id);
        }

        public virtual T GetById(string id)
        {
            return DataContext.SingleOrDefaultById<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DataContext.FetchWhere<T>(null);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DataContext.FetchWhere<T>(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return DataContext.FetchWhere<T>(where).FirstOrDefault();
        }
    }
}
