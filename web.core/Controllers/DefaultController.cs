using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using web.core.Authentication;
using web.core.Repositories;

namespace web.core.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        [Inject]
        private IFormsAuthentication formAuthentication { get; set; }
        //
        // GET: /Home/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(web.core.Models.LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserInfo userInfo = new UserInfo
                {
                    UserId = 1,
                    DisplayName = model.UserName,
                    RoleName = "Admin,User"
                };

                formAuthentication.SetAuthCookie(this.HttpContext, UserAuthenticationTicketBuilder.CreateAuthenticationTicket(userInfo));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult Logout()
        {
            formAuthentication.Signout();
            return Redirect("/");
        }
    }
}
