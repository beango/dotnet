
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the Territories table in the Northwind Database.
    /// </summary>
    public partial class Territory
    {
		#region Column Mappings

		public string TerritoryID{get;set;}

		public string TerritoryDescription{get;set;}

		public int RegionID{get;set;}

		#endregion
    }
}
