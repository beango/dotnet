
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the Order Details table in the Northwind Database.
    /// </summary>
    public partial class OrderDetail
    {
		#region Column Mappings

		public int OrderID{get;set;}

		public int ProductID{get;set;}

		public decimal UnitPrice{get;set;}

		public short Quantity{get;set;}

		public decimal Discount{get;set;}

		#endregion
    }
}
