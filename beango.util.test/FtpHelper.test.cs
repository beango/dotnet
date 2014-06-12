using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beango.util.test
{
    [TestClass]
    public class FtpHelper_Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestHttpContext mock = new TestHttpContext(false);

            var ftp = new FtpHelper("ftp://localhost:801", "administrator", "1");

            ftp.upload("ftp://localhost:801/test/2.txt","c://2.txt");
        }
    }
}
