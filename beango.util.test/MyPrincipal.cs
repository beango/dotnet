using System.Security.Principal;

namespace beango.util.test
{
    public class MyPrincipal : IPrincipal //模拟的IPrincipal接口  
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity
        {
            get { return new MyIdentity(); }
        }
    }


    public class MyIdentity : IIdentity
    {
        public string Name
        {
            get { return "单元测试用户"; }
        }

        public string AuthenticationType
        {
            get { return "单元测试用户"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}
