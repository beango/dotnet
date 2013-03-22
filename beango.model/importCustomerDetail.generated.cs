
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the importCustomerDetail table in the Northwind Database.
    /// </summary>
    public partial class importCustomerDetail
    {
		#region Column Mappings

		public int pkid{get;set;}

		public int importHisId{get;set;}

		public string tradeCode{get;set;}

		public string name{get;set;}

		public decimal? tradeAmt{get;set;}

		public string contacter{get;set;}

		public string tel{get;set;}

		public string mobile{get;set;}

		public string bank{get;set;}

		public string bankAccount{get;set;}

		public string contractStatus{get;set;}

		public DateTime? creation_date{get;set;}

		public string customerFrom{get;set;}

		public string customerType{get;set;}

		public string identityCode{get;set;}

		public string status{get;set;}

		public DateTime? importDate{get;set;}

		public int? importBy{get;set;}

		#endregion
    }
}
