






































using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the importFileHis table in the Northwind Database.
    /// </summary>
    public partial class importFileHi
    {
		#region Column Mappings

		public int pkid{get;set;}

		public string fileName{get;set;}

		public int excelType{get;set;}

		public DateTime ctime{get;set;}

		public int opUserId{get;set;}

		public string DoneRemark{get;set;}

		#endregion
    }
}
