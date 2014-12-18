using common;
using dal.ef.core;
using model.ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Repositories
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory,System.Data.Entity.DbContext dbContext)
            : base(databaseFactory,dbContext)
        {

        }

        //[InterceptCache(TimeOut = 2)]
        public IEnumerable<Products> GetAll(int pageindex, int pagesize, out int recordcount)
        {
            recordcount = base.dbset.Count();
            return base.dbset.OrderByDescending(o => o.ProductID)
                .Skip((pageindex - 1) * pagesize).Take(pagesize);
        }
    }

    public interface IProductRepository : IRepository<Products>
    {
        IEnumerable<Products> GetAll(int pageindex, int pagesize, out int recordcount);
    }
}