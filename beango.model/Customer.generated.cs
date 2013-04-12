
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
    /// A class which represents the Customers table in the Northwind Database.
    /// </summary>
    public partial class Customers
    {
		#region Column Mappings
		
		public string CustomerID{get;set;}
		
		public string CompanyName{get;set;}
		
		public string ContactName{get;set;}
		
		public string ContactTitle{get;set;}
		
		public string Address{get;set;}
		
		public string City{get;set;}
		
		public string Region{get;set;}
		
		public string PostalCode{get;set;}
		
		public string Country{get;set;}
		
		public string Phone{get;set;}
		
		public string Fax{get;set;}

		#endregion
    }
}
