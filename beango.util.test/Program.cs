using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using beango.model;
using fastJSON;

namespace beango.util.test
{
    class Program
    {
        static void Main(string[] args)
        {
            var prolist = new Products()
                              {
                                  ProductID = 1
                              };
            var jsonstr = JSON.Instance.ToJSON(prolist);
            Console.WriteLine(jsonstr);

            var p = JSON.Instance.ToObject<Products>(jsonstr);
            Console.WriteLine(p.ProductID);
        }
    }

    [XmlRoot("products")]
    public class productlist
    {
        //[XmlArrayItem("product")]
        //[XmlArray("cccccccccccc")]
        [XmlElement("product")]
        public List<Products> products { get; set; }
    }
}
