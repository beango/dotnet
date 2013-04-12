
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
    /// A class which represents the UserInfo table in the Northwind Database.
    /// </summary>
    public partial class UserInfo
    {
		#region Column Mappings
		[Key]
		public int UserID{get;set;}
		
		public string UserName{get;set;}
		
		public string UserPwd{get;set;}

		#endregion
    }
}
