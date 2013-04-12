
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
    /// A class which represents the CustomerDemographics table in the Northwind Database.
    /// </summary>
    public partial class CustomerDemographics
    {
		#region Column Mappings
		
		public string CustomerTypeID{get;set;}
		
		public string CustomerDesc{get;set;}

		#endregion
    }
}
