//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace beango.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<long> UnitsInStock { get; set; }
        public Nullable<long> UnitsOnOrder { get; set; }
        public Nullable<long> ReorderLevel { get; set; }
        public string Discontinued { get; set; }
    }
}
