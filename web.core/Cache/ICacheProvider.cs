using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.core.Cache
{
    public interface ICacheProvider
    {
        object Get(string key);

        void Set(string key, object data, double expiry);
    }
}