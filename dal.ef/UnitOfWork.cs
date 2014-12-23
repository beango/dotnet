using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal
{
    public class UnitOfWork : IUnitOfWork
    {
        [Inject]
        private DbContext DataContext { get; set; }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}
