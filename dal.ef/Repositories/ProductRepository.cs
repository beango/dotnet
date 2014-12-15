using dal.ef.core;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.ef.Repositories
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository()
        {
        }
    }

    public interface IProductRepository : IRepository<Products>
    {
    }    
}
