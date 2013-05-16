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
            dt.Columns.Add(new DataColumn("productID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Producttype", typeof(System.String)));
            dt.Columns.Add(new DataColumn("CreateDate", typeof(System.DateTime)));
            dt.Columns.Add(new DataColumn("Price", typeof(System.Decimal)));
            dt.Columns.Add(new DataColumn("Dishibited", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("desc", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Addres", typeof(System.String)));

            for (int i = 1; i <= _rows; i++)
            {
                DataRow dr = dt.NewRow();
                dr["productID"] = i;
                dr["Producttype"] = "电器";
                dr["CreateDate"] = DateTime.Now;
                dr["Price"] = 12.41;
                dr["dishibited"] = true;
                dr["desc"] = "Male" + i.ToString();
                dr["Addres"] = "Addres" + i.ToString();

                dt.Rows.Add(dr);
            }

            return dt;

        }

        static void Main(string[] args)
        {
            Console.WriteLine(System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion());

            const int count = 100000;
            DataTable dt = GetCustomer(count);
            var testObj = new product();

            /**********************************************/
            Console.Write("直接访问花费时间：".PadRight(20, ' '));
            Stopwatch watch1 = Stopwatch.StartNew();

            testObj = GetEntity(dt).FirstOrDefault();

            watch1.Stop();
            Console.WriteLine(watch1.Elapsed.ToString());

            /**********************************************/
            Console.Write("反射生成实体花费时间：".PadRight(18, ' '));
            Stopwatch watch2 = Stopwatch.StartNew();

            testObj = new ToEntityByReflection<product>().GetEntity(dt).FirstOrDefault(); 

            watch2.Stop();
            Console.WriteLine(watch2.Elapsed.ToString());

            /**********************************************/
            Console.Write("Delegate生成实体花费时间：".PadRight(20, ' '));
            Stopwatch watch3 = Stopwatch.StartNew();

            testObj = new ToEntityByDelegate<product>().GetEntity(dt).FirstOrDefault(); 

            watch3.Stop();
            Console.WriteLine(watch3.Elapsed.ToString());

            /**********************************************/
            Console.Write("Expression生成实体花费时间：".PadRight(20, ' '));
            Stopwatch watch4 = Stopwatch.StartNew();

            testObj = ToEntityByExpression.GetEntity<product>(dt).FirstOrDefault(); 

            watch4.Stop();
            Console.WriteLine(watch4.Elapsed.ToString());

            /**********************************************/
            Console.Write("Emit生成实体花费时间：".PadRight(20, ' '));
            Stopwatch watch5 = Stopwatch.StartNew();

            testObj = dt.ToList<product>().FirstOrDefault(); 

            watch5.Stop();
            Console.WriteLine(watch5.Elapsed.ToString());
        }

        /// <summary>
        /// 直接赋值的方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static IEnumerable<product> GetEntity(DataTable dt)
        {
            List<product> lst = new List<product>();
            foreach (DataRow dr in dt.Rows)
            {
                product cus = new product();
                cus.ProductID = int.Parse(dr["ProductID"].ToString());
                //cus.Producttype = (producttype)Enum.Parse(typeof(producttype), dr["Producttype"].ToString());
                cus.CreateDate = (dr["CreateDate"] is DateTime)?DateTime.Parse(dr["CreateDate"].ToString()):new DateTime?();
                cus.Dishibited = Boolean.Parse(dr["Dishibited"].ToString());
                cus.Price = (dr["Price"] is decimal) ? decimal.Parse(dr["Price"].ToString()) : 0; 
                cus.desc = dr["desc"].ToString();

                lst.Add(cus);
            }
            return lst;
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

    public class product
    {
        public int ProductID { get; set; }
        //public producttype Producttype { get; set; }
        public DateTime? CreateDate { get; set; }
        public decimal Price { get; set; }
        public bool Dishibited { get; set; }
        public string desc { get; set; }

        public override string ToString()
        {
            return string.Format("PID：{0}、T：{1}、CD：{2}、P：{3}、D：{4}、desc：{5}",
                ProductID.ToString(), "", CreateDate.ToString(), Price.ToString(), Dishibited.ToString(), desc);
        }
    }

    public enum producttype
    {
        汽车,
        电器
    }
}