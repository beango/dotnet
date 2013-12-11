using System;
using System.IO;

namespace beango.util.test
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Reading login data...");


                var loginData = File.ReadAllLines("Login.txt");

                string hostAddress = loginData[0];
                string username = loginData[1];
                string password = loginData[2];

                var ftp = new Ftputil(hostAddress, username, password);
                //ftp.createFolder("test1");
                Console.WriteLine(ftp.existsFolder("test1"));
                //ftp.deleteFolder("test1");
                ftp.upload(new FileStream("Login.txt",FileMode.Open,FileAccess.Read));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
