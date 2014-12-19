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
    public class SupplierRepository: RepositoryBase<Suppliers>, ISupplierRepository
    {
        //public SupplierRepository( DbContext context)
        //    : base(context)
        //{

        //}
    }

    public interface ISupplierRepository : IRepository<Suppliers>
    {
    }
}
