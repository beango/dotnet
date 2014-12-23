using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private NorthwindContext dataContext;
        public NorthwindContext Get()
        {
            return dataContext ?? (dataContext = new NorthwindContext());
        }
        public void Dispose()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
