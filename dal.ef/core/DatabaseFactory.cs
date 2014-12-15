using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.ef.core
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private NorthwindContext dataContext;

        public NorthwindContext Get()
        {
            return dataContext ?? (dataContext = new NorthwindContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
