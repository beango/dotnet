using common;
using dal;
using model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Repositories
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        //[InterceptCache(TimeOut = 2)]
        public IEnumerable<Products> GetAll(string q, int pageindex, int pagesize, out int recordcount)
        {
            recordcount = 100;
            return GetAll().OrderByDescending(model=>model.ProductID);
            //var dbset = base.DataContext.Set<Products>();
            //recordcount = dbset.Count();
            //return dbset.Where(o=>o.ProductName.IndexOf(q)>-1).OrderByDescending(o => o.ProductID)
            //    .Skip((pageindex - 1) * pagesize).Take(pagesize);
        }
    }

    public interface IProductRepository : IRepository<Products>
    {
        IEnumerable<Products> GetAll(string q, int pageindex, int pagesize, out int recordcount);
    }
}