using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beango.util.test
{
    [TestClass]
    public class CookieHelper_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string testcokkey = "testcookie";
            const string testcokval = "testcookie_value";
            new CookieHelper().SetCookie(testcokkey, testcokval);

            Assert.IsNotNull(new CookieHelper().GetCookie(testcokkey));

            Assert.AreEqual(testcokval,new CookieHelper().GetCookie(testcokkey));

            const string ntestcokval = "testcookie_value_new";
            new CookieHelper().SetCookie(testcokkey, ntestcokval);

            Assert.AreEqual(ntestcokval, new CookieHelper().GetCookie(testcokkey));

            new CookieHelper().ClearCookie(ntestcokval);

            Assert.IsNull(new CookieHelper().GetCookie(testcokkey));
        }
    }
}
