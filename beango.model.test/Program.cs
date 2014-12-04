using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beango.model.test
{
    class Program
    {
        static void Main(string[] args)
        {
            ORCLEntities entity = new ORCLEntities();
            var rst = entity.PRO_TEST2();
            Console.WriteLine(rst.Count());
        }
    }
}
