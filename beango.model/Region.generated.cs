
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
    /// A class which represents the Region table in the Northwind Database.
    /// </summary>
    public partial class Region
    {
		#region Column Mappings
		
		public int RegionID{get;set;}
		
		public string RegionDescription{get;set;}

		#endregion
    }
}
