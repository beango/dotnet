using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core.Models
{
    public class ProductsModel
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public Nullable<int> SupplierID { get; set; }

        [Required]
        public Nullable<int> CategoryID { get; set; }

        [Required]
        public string QuantityPerUnit { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }

        public Nullable<short> UnitsInStock { get; set; }

        public Nullable<short> UnitsOnOrder { get; set; }

        public Nullable<short> ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public CategoryModel Categories { get; set; }
        public SupplierModel Suppliers { get; set; }
    }
}
