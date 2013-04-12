using System.Data.Entity;
using beango.model;

namespace MvcAjaxForm.Models
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Products> Products { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<UserInfo> UserList { get; set; }
    }
}
