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
    
    public partial class Invoices
    {
        public long OrderID { get; set; }
        public string CustomerID { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public long Quantity { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public double Discount { get; set; }
        public string City { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public string ShipName { get; set; }
        public string Country { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}