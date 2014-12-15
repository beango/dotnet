using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public partial class NorthwindContext
    {
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
