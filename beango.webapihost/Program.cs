using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using beango.util;
using System.ServiceModel;

namespace beango.webapihost
{
    class Program
    {
        static void Main(string[] args)
        {
            InitService();

            if (_config != null)
            {
                _server = new HttpSelfHostServer(_config);
                _server.OpenAsync().Wait();
                Console.WriteLine("服务已经启动，绑定地址："+ServiceAddress);
            }
            else
            {
                Console.WriteLine("启动不成功");
            }
            Console.ReadKey();
        }

        private static HttpSelfHostServer _server;
        private static HttpSelfHostConfiguration _config;
        public const string ServiceAddress = "http://localhost:8346";
        private static void InitService()
        {
            try
            {
                _config = new HttpSelfHostConfiguration(ServiceAddress);
                _config.MaxReceivedMessageSize = int.MaxValue;
                _config.TransferMode = TransferMode.Buffered;
                _config.Routes.MapHttpRoute("DefaultApi",
                    "{controller}/{action}",
                    new { controller = "FileUpload", action = "Hello", id = RouteParameter.Optional });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.Error(ex);
            }
        }
    }
}
