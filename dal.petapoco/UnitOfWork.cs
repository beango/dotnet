using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            //DataContext.SaveChanges();
        }
    }
}
