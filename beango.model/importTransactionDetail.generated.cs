
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the importTransactionDetail table in the Northwind Database.
    /// </summary>
    public partial class importTransactionDetail
    {
		#region Column Mappings

		public int pkid{get;set;}

		public int importHisId{get;set;}

		public long tradeNo{get;set;}

		public string tradeCode{get;set;}

		public string productCode{get;set;}

		public string tradeType{get;set;}

		public decimal? tradePrice{get;set;}

		public int? tradeQty{get;set;}

		public DateTime? tradeTime{get;set;}

		public decimal? tradeAmt{get;set;}

		public DateTime? importDate{get;set;}

		public int? importBy{get;set;}

		#endregion
    }
}
