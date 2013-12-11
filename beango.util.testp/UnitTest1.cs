using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace beango.util.testp
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: 在此处添加测试逻辑
            //
            var s = Convert.ToInt32("123");
            Assert.AreEqual(s, 123);

            var loginData = Properties.Resources.Login.Split('\n');

            string hostAddress = loginData[0].Trim();
            string username = loginData[1].Trim();
            string password = loginData[2].Trim();

            var ftp = new Ftputil(hostAddress, username, password);

            if (ftp.existsFolder("test1"))
            {
                ftp.deleteFolder("test1");
                Assert.AreEqual(ftp.existsFolder("test1"), false);

                ftp.createFolder("test1");
                Assert.AreEqual(ftp.existsFolder("test1"), true);
            }
            else
            {
                ftp.createFolder("test1");
                Assert.AreEqual(ftp.existsFolder("test1"), true);

                ftp.deleteFolder("test1");
                Assert.AreEqual(ftp.existsFolder("test1"), false);
            }

            var databytes = Encoding.UTF8.GetBytes(Properties.Resources.Login);
            var fs = new FileStream("temp.txt", FileMode.Create);
            
            fs.Write(databytes, 0, databytes.Length);
            //int start = 0;
            //while (start < databytes.Length)
            //{
            //    fs.Write(databytes, 0, 1);
            //    start += 1;
            //}
            Console.WriteLine(fs.Length); fs.Close();
            fs = new FileStream("temp.txt", FileMode.Open);
            ftp.upload(fs);
            fs.Close();
            fs = null;
            //Assert.AreEqual(ftp.existsFolder("test1"), false);
        }
    }
}
