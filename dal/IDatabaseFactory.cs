using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal
{
    public interface IDatabaseFactory : IDisposable
    {
        NorthwindContext Get();
    }
}
