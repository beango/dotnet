
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the EmployeeTerritories table in the Northwind Database.
    /// </summary>
    public partial class EmployeeTerritory
    {
		#region Column Mappings

		public int EmployeeID{get;set;}

		public string TerritoryID{get;set;}

		#endregion
    }
}
