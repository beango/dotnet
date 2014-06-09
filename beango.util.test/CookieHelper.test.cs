using System;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beango.util.test
{
    [TestClass]
    public class CookieHelper_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestHttpContext mock = new TestHttpContext(false);
            
            const string testcokkey = "testcookie";
            const string testcokval = "testcookie_value";
            new CookieHelper().SetCookie(testcokkey, testcokval);

            //Assert.IsNotNull(new CookieHelper().GetCookie(testcokkey));

            //Assert.AreEqual(testcokval, new CookieHelper().GetCookie(testcokkey));

            //const string ntestcokval = "testcookie_value_new";
            //new CookieHelper().SetCookie(testcokkey, ntestcokval);

            //Console.WriteLine(new CookieHelper().GetCookie(testcokkey));
            //Assert.AreEqual(ntestcokval, new CookieHelper().GetCookie(testcokkey));

            new CookieHelper().RemoveCookie(testcokkey);
            HttpCookie cok = HttpContext.Current.Request.Cookies[testcokkey];
            if(null!=cok)
            cok.Values.Remove(testcokkey);

            //Assert.IsNull(new CookieHelper().GetCookie(testcokkey));

            Console.WriteLine(new CookieHelper().GetCookie(testcokkey));
        }
    }
}
