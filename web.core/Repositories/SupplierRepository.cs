using dal.ef.core;
using model.ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Repositories
{
    public class SupplierRepository: RepositoryBase<Suppliers>, ISupplierRepository
    {
        public SupplierRepository(IDatabaseFactory databaseFactory, System.Data.Entity.DbContext dbContext)
            : base(databaseFactory,dbContext)
        {

        }
    }

    public interface ISupplierRepository : IRepository<Suppliers>
    {
    }
}
