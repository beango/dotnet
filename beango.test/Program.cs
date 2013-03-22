using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using beango.dal.PetaPoco;
using beango.model;

namespace beango.test
{
    class Program
    {
        static Database db;

        static void Main(string[] args)
        {
            db = new Database("ResourceSystem");

            var posts = db.Fetch<MessageLog>("SELECT * FROM MessageLog");

            Console.WriteLine(posts.Count);
        }
    }
}
