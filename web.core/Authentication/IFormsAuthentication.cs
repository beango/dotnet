using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace web.core.Authentication
{
    public interface IFormsAuthentication
    {
        void Signout();
        void SetAuthCookie(HttpContextBase httpContext, FormsAuthenticationTicket authenticationTicket);
        void SetAuthCookie(HttpContext httpContext, FormsAuthenticationTicket authenticationTicket);
        FormsAuthenticationTicket Decrypt(string encryptedTicket);
    }
}
