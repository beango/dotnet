






































using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace beango.model
{
    /// <summary>
    /// A class which represents the Orders table in the Northwind Database.
    /// </summary>
    public partial class Orders
    {
		#region Column Mappings
		[Key]
		public int OrderID{get;set;}
		
		public string CustomerID{get;set;}
		
		public int? EmployeeID{get;set;}
		
		public DateTime? OrderDate{get;set;}
		
		public DateTime? RequiredDate{get;set;}
		
		public DateTime? ShippedDate{get;set;}
		
		public int? ShipVia{get;set;}
		
		public decimal? Freight{get;set;}
		
		public string ShipName{get;set;}
		
		public string ShipAddress{get;set;}
		
		public string ShipCity{get;set;}
		
		public string ShipRegion{get;set;}
		
		public string ShipPostalCode{get;set;}
		
		public string ShipCountry{get;set;}

		#endregion
    }
}
