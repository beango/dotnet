
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the Categories table in the Northwind Database.
    /// </summary>
    public partial class Category
    {
		#region Column Mappings

		public int CategoryID{get;set;}

		public string CategoryName{get;set;}

		public string Description{get;set;}

		public byte[] Picture{get;set;}

		#endregion
    }
}
