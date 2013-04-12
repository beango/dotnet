
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
    /// A class which represents the CustomerCustomerDemo table in the Northwind Database.
    /// </summary>
    public partial class CustomerCustomerDemo
    {
		#region Column Mappings
		
		public string CustomerID{get;set;}
		
		public string CustomerTypeID{get;set;}

		#endregion
    }
}
