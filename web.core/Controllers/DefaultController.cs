using common;
using dal;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using web.core.Authentication;
using web.core.Repositories;

namespace web.core.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : BaseController<object>
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

        [Inject]
        public IRepository<test> testRepository { get; set; }

        [AllowAnonymous]
        public ActionResult test()
        {
            try
            {
                //NorthwindContext dataContext = new NorthwindContext();
                //var s = new test() { c2 = 1 };
                //dataContext.Set<test>().Add(s);
                //dataContext.SaveChanges();

                //return Content(s.c2.ToString());

                var c = testRepository.Get(o => o.c1 == 1);
                c.c2 += 1;
                testRepository.Update(c);
                unitOfWork.Commit();
                return Content(c.c2.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return Content(ex.Message);
            }
        }
    }
}
