
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the Products table in the Northwind Database.
    /// </summary>
    public partial class Product
    {
		#region Column Mappings

		public int ProductID{get;set;}

		public string ProductName{get;set;}

		public int? SupplierID{get;set;}

		public int? CategoryID{get;set;}

		public string QuantityPerUnit{get;set;}

		public decimal? UnitPrice{get;set;}

		public short? UnitsInStock{get;set;}

		public short? UnitsOnOrder{get;set;}

		public short? ReorderLevel{get;set;}

		public bool Discontinued{get;set;}

		#endregion
    }
}
