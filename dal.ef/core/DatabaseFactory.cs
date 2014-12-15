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
        private futuresEntities dataContext;
        public futuresEntities Get()
        {
            return dataContext ?? (dataContext = new futuresEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
