using dal.ef.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using web.Models;

namespace web.Controllers
{
    [Authorize(Roles = "admin")]
    public class DefaultController : Controller
    {
        [Inject]
        public IProductRepository productRepository{get;set;}

        //
        // GET: /Home/
        [Authorize(Roles = "test")]
        public ActionResult Index()
        {
            return View(productRepository.GetAll());
        }

        //
        // GET: /Home/
        [Authorize(Roles = "admin2")]
        public ActionResult Index2()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                                                                                     model.UserName,
                                                                                     DateTime.Now,
                                                                                     DateTime.Now.AddMinutes(20),
                                                                                     false,
                                                                                     "admin,test" //自定义数据  
                    );
                //对authTicket进行加密  
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                //存入cookie  
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);  

                return RedirectToAction("Index", "Default");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
    }
}
