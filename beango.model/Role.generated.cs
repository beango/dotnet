
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
    /// A class which represents the Roles table in the Northwind Database.
    /// </summary>
    public partial class Roles
    {
		#region Column Mappings
		[Key]
		public int RoleId{get;set;}
		
		public string RoleName{get;set;}

		#endregion
    }
}
