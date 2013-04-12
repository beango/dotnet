
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
    /// A class which represents the Shippers table in the Northwind Database.
    /// </summary>
    public partial class Shippers
    {
		#region Column Mappings
		[Key]
		public int ShipperID{get;set;}
		
		public string CompanyName{get;set;}
		
		public string Phone{get;set;}

		#endregion
    }
}
