using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace beango.ninject
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = KernelConfiguration.Kernel.Get<IService>();
            service.SomeMethod("hello");
            Console.ReadKey();
        }
    }
}
