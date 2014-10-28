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
    public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseFactory databaseFactory;
        private DbContext dataContext;

        public UnitOfWork(IDatabaseFactory _databaseFactory)
        {
            this.databaseFactory = _databaseFactory;
        }
        protected DbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}
