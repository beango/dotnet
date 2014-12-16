using common;
using dal.ef.core;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Repositories
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        [InterceptCache(TimeOut = 2)]
        public List<Products> GetAll2()
        {
            return base.GetAll().ToList();
        }
    }

    public interface IProductRepository : IRepository<Products>
    {
        List<Products> GetAll2();
    }
}