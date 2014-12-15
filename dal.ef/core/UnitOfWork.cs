using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.ef.core
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseFactory databaseFactory;
        private NorthwindContext dataContext;

        public UnitOfWork(IDatabaseFactory _databaseFactory)
        {
            this.databaseFactory = _databaseFactory;
        }
        protected NorthwindContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
