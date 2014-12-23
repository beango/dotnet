using common;
using dal;
using model;
using Ninject;
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
            return Page(null, pageindex, pagesize, out recordcount);
        }
    }

    public interface IProductRepository : IRepository<Products>
    {
        IEnumerable<Products> GetAll(string q, int pageindex, int pagesize, out int recordcount);
    }
}