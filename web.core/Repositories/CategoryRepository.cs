using dal.ef.core;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Repositories
{
    public class CategoryRepository : RepositoryBase<Categories>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseFactory databaseFactory, System.Data.Entity.DbContext dbContext)
            : base(databaseFactory,dbContext)
        {

        }
    }

    public interface ICategoryRepository : IRepository<Categories>
    {

    }
}
