
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
    /// A class which represents the MessageLog table in the Northwind Database.
    /// </summary>
    public partial class MessageLog
    {
		#region Column Mappings
		[Key]
		public int id{get;set;}
		
		public string Number{get;set;}
		
		public string Content{get;set;}
		
		public string CustomerID{get;set;}
		
		public bool SendFlag{get;set;}
		
		public DateTime? SendTime{get;set;}
		
		public DateTime CreateDate{get;set;}
		
		public int CreateUser{get;set;}
		
		public int ErrNum{get;set;}
		
		public string SendSummary{get;set;}
		
		public int ClientID{get;set;}
		
		public int MsgType{get;set;}

		#endregion
    }
}
