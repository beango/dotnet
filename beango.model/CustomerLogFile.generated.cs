
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the CustomerLogFile table in the Northwind Database.
    /// </summary>
    public partial class CustomerLogFile
    {
		#region Column Mappings

		public int id{get;set;}

		public string filename{get;set;}

		public bool importflag{get;set;}

		public DateTime ctime{get;set;}

		#endregion
    }
}
