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
    public class CategoryRepository : RepositoryBase<Categories>, ICategoryRepository
    {

    }

    public interface ICategoryRepository : IRepository<Categories>
    {

    }
}
