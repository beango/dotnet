
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace beango.model
{
    /// <summary>
    /// A class which represents the importCustomerCapitalCount table in the Northwind Database.
    /// </summary>
    public partial class importCustomerCapitalCount
    {
		#region Column Mappings

		public int pkid{get;set;}

		public int importHisId{get;set;}

		public string tradeCode{get;set;}

		public decimal? initAmt{get;set;}

		public decimal? deposit{get;set;}

		public decimal? outAmt{get;set;}

		public decimal? securityDepositChanged{get;set;}

		public decimal? floatAmtChanged{get;set;}

		public decimal? deliveryAmtChanged{get;set;}

		public decimal? transferGAL{get;set;}

		public decimal? deliveryGAL{get;set;}

		public decimal? salesAmt{get;set;}

		public decimal? buyAmt{get;set;}

		public decimal? tradeAmt{get;set;}

		public decimal? deliveryAmt{get;set;}

		public decimal? extenDeliveryAmt{get;set;}

		public decimal? otherItem{get;set;}

		public decimal? endAmt{get;set;}

		public decimal? curRights{get;set;}

		public DateTime? importDate{get;set;}

		public int? importBy{get;set;}

		#endregion
    }
}
