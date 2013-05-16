using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using beango.dal;
using beango.model;
using beango.northwindmvc.Module;
using beango.util;
using fastJSON;

namespace beango.northwindmvc.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(IDao<User> dao)
            : base(dao)
        {

        }

        [LoginLess]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [LoginLess]
        public ActionResult Login(string username, string pwd, string remeber, string c, string a)
        {
            try
            {
                var user = dao.GetEntity(obj => obj.UserName == username);
                if (user == null)
                {
                    return Json(new { result = false, desc = "登录失败！" });
                }
                DateTime Expires = DateTime.Now;
                if (remeber == "on")
                    Expires = DateTime.Now.AddDays(365);
                IUserState us = new UserAuthModule();
                us.UserID = user.UserID;
                us.UserName = user.UserName;

                CookieHelper cookieHelper = new CookieHelper();
                if (remeber == "on")
                    cookieHelper.SetCookie("User", JSON.Instance.ToJSON(us), Expires);
                else
                    cookieHelper.SetCookie("User", JSON.Instance.ToJSON(us));
                if (!string.IsNullOrEmpty(a))
                {
                    return Json(new { result = true, desc = "/" + c + "/" + a });
                }
                return Json(new { result = true, desc = "/Product/List" });
            }
            catch (Exception e)
            {
                return Json(new { result = false, desc = "系统错误:" + e.Message });
            }
        }

        public ActionResult Logout()
        {
            new CookieHelper().ClearCookie("User");
            return Redirect("/Product/List");
        }

        public ActionResult NoAuth()
        {
            return View();
        }
    }
}
