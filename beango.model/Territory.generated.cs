
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
    /// A class which represents the Territories table in the Northwind Database.
    /// </summary>
    public partial class Territories
    {
		#region Column Mappings
		
		public string TerritoryID{get;set;}
		
		public string TerritoryDescription{get;set;}
		
		public int RegionID{get;set;}

		#endregion
    }
}
