using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public partial class Products
    {
        [NPoco.ResultColumn]
        public Categories Categories { get; set; }

        [NPoco.ResultColumn]
        public Suppliers Suppliers { get; set; }
    }
}
