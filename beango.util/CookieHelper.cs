using System;
using System.Web;

namespace beango.util
{
    /*
     * Http Cookie辅助类
     */
    public class CookieHelper
    {
        //设置Cookies
        public void SetCookie(string key, string value)
        {
            HttpCookie Cookie = new HttpCookie(key);
            Cookie.Values.Add(key, value);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        //设置Cookies
        public void SetCookie(string key, string value, DateTime Expires)
        {
            HttpCookie Cookie = new HttpCookie(key);
            Cookie.Values.Add(key, value);
            Cookie.Expires = Expires;
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        //获取Cookie
        public string GetCookie(string key)
        {
            var httpCookie = HttpContext.Current.Request.Cookies[key];
            if (httpCookie != null) return httpCookie.Values[httpCookie.Values.Count-1];
            return null;
        }

        public void ClearCookie(string key)
        {
            HttpCookie Cookie = new HttpCookie(key);
            Cookie.Values.Add(key, "");
            Cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }
    }
}
