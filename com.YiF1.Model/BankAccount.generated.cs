






































using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace com.YiF1.Model
{
    /// <summary>
    /// A class which represents the BankAccount table in the Northwind Database.
    /// </summary>
    public partial class BankAccount
    {
		#region Column Mappings

		public int Id{get;set;}

		public string Bank{get;set;}

		public string Name{get;set;}

		public string Address{get;set;}

		public string Account{get;set;}

		public bool IsPublic{get;set;}

		public DateTime CTime{get;set;}

		public DateTime UTime{get;set;}

		public bool IsShow{get;set;}

		#endregion
    }
}
