using beango.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace beango.speedm
{
    public class Program
    {
        public static DataTable GetCustomer(int _rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("SupplierID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("UnitsOnOrder");
            dt.Columns.Add("ReorderTime");
            dt.Columns.Add("Discontinued");

            Random r = new Random();
            
            for (int i = 1; i <= _rows; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ProductID"] = (Int64.MaxValue - 100000)+i;
                dr["ProductName"] = "电器" + (Int64.MaxValue - 100000) + i;
                dr["SupplierID"] = (Int64.MaxValue - 100000) + i;
                dr["CategoryID"] = r.Next(Int16.MaxValue);
                dr["UnitPrice"] = decimal.MaxValue-100000+i;
                dr["UnitsOnOrder"] = byte.MaxValue;
                dr["ReorderTime"] = DateTime.Now.AddMinutes(i);
                dr["Discontinued"] = false;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static void ShowMsg(Products dr)
        {
            string s = "";
            s += "ProductID: " + dr.ProductID;
            s += "； ProductName" + dr.ProductName;
            s += "； SupplierID" + dr.SupplierID;
            s += "； CategoryID" + dr.CategoryID;
            s += "； UnitPrice" + dr.UnitPrice;
            s += "； UnitsOnOrder" + dr.UnitsOnOrder;
            s += "； ReorderTime" + dr.ReorderTime;
            s += "； Discontinued" + dr.Discontinued;

            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion());

            const int count = 100000;
            DataTable dt = GetCustomer(count);
            var testObj = new Products();

            /**********************************************/
            Console.Write("反射生成实体花费时间：".PadRight(18, ' '));
            Stopwatch watch2 = Stopwatch.StartNew();

            testObj = new ToEntityByReflection<Products>().GetEntity(dt).FirstOrDefault();
            ShowMsg(testObj);
            watch2.Stop();
            Console.WriteLine(watch2.Elapsed.ToString());        

            /**********************************************/
            Console.Write("Expression生成实体花费时间：".PadRight(20, ' '));
            Stopwatch watch4 = Stopwatch.StartNew();

            testObj = ToEntityByExpression.GetEntity<Products>(dt).FirstOrDefault();
            ShowMsg(testObj);
            watch4.Stop();
            Console.WriteLine(watch4.Elapsed.ToString());

            /**********************************************/
            Console.Write("Emit生成实体花费时间：".PadRight(20, ' '));
            Stopwatch watch5 = Stopwatch.StartNew();

            testObj = dt.ToList<Products>().FirstOrDefault();
            ShowMsg(testObj);
            watch5.Stop();
            Console.WriteLine(watch5.Elapsed.ToString());
            Console.ReadKey();
        }
    }

    public static class util
    {
        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            List<TResult> list = new List<TResult>();
            if (dt == null) return list;
            DataTableEntityBuilder<TResult> eblist = DataTableEntityBuilder<TResult>.CreateBuilder(dt.Rows[0]);
            list.AddRange(from DataRow info in dt.Rows select eblist.Build(info));
            dt.Dispose(); dt = null;
            return list;
        }
    }
   
}