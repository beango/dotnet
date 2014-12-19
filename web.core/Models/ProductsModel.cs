using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace web.core.Models
{
    public class ProductsModel
    {
        [ScaffoldColumn(false)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户ID不能为空")]
        [Display(Name = "产品ID")]
        public int ProductID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "产品名称不能为空")]
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "供应商不能为空")]
        [Display(Name = "供应商")]
        public Nullable<int> SupplierID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "分类不能为空")]
        [Display(Name = "分类")]
        public Nullable<int> CategoryID { get; set; }

        [Display(Name = "单位数量")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "单价")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> UnitPrice { get; set; }

        [Display(Name = "存量")]
        public Nullable<short> UnitsInStock { get; set; }

        [Display(Name = "订单量")]
        public Nullable<short> UnitsOnOrder { get; set; }

        [Display(Name = "级别")]
        public Nullable<short> ReorderLevel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "是否停产不能为空")]
        [Display(Name = "停产")]
        public bool Discontinued { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "创建时间不能为空")]
        [Display(Name = "创建时间")]
        [ReadOnly(true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public System.DateTime CreateTime { get; set; }

        public CategoryModel Categories { get; set; }
        public SupplierModel Suppliers { get; set; }
    }
}
