
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
    /// A class which represents the UsersRole table in the Northwind Database.
    /// </summary>
    public partial class UsersRole
    {
		#region Column Mappings
		[Key]
		public int Id{get;set;}
		
		public int UserID{get;set;}
		
		public int RoleID{get;set;}

		#endregion
    }
}
