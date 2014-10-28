using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.ef.core
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        [Inject]
        public DbContext dataContext{get;set;}

        public DbContext Get()
        {
            return dataContext;//?? (dataContext = new NorthwindContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
