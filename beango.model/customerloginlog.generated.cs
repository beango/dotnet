






































using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the customerloginlog table in the Northwind Database.
    /// </summary>
    public partial class customerloginlog
    {
		#region Column Mappings

		public int id{get;set;}

		public string uid{get;set;}

		public int? cid{get;set;}

		public string mac{get;set;}

		public string cpu{get;set;}

		public string diskid{get;set;}

		public string exemd5{get;set;}

		public string opt{get;set;}

		public string otherinfo{get;set;}

		public bool? actiontype{get;set;}

		public int? campaignId{get;set;}

		public DateTime? time{get;set;}

		public string token{get;set;}

		#endregion
    }
}
