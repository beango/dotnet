using System;
using System.Web.Mvc;
using Ninject;
using beango.dal;
using beango.northwindmvc.Module;
using beango.util;
using fastJSON;

namespace beango.northwindmvc.Controllers
{
    public class BaseController : Controller
    {
        #region 权限

        /// <summary>
        /// 用户信息
        /// </summary>
        [Inject]
        public IUserState UserState { set; get; }

        #endregion
    }

    public class BaseController<T> : BaseController where T : class
    {
        #region dao

        public IDao<T> dao;
        public BaseController(IDao<T> dao)
        {
            this.dao = dao;
        }

        /// <summary>  
        /// 根据多个条件进行查询  
        /// </summary>  
        /// <typeparam name="T">实体类型</typeparam>  
        /// <returns>符合条件的实体的集合</returns>  
        public virtual ActionResult List()
        {
            return View(dao.FindList());
        }

        //
        // GET: /Base/Details/5
        public virtual ActionResult Details(int id)
        {
            return View(dao.GetEntityByKey(id));
        }

        //
        // GET: /Base/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Base/Create
        [HttpPost]
        public virtual ActionResult Create(T obj)
        {
            try
            {
                dao.AddObject(obj);
                string resultText = "添加成功";
                return Json(new { Result = true, Text = resultText });
            }
            catch (Exception exception)
            {
                return Json(new { Result = false, Text = exception.Message });
            }
        }

        //
        // GET: /Base/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View(dao.GetEntityByKey(id));
        }

        //
        // POST: /Base/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(T obj)
        {
            try
            {
                dao.UpdateObject(obj);

                return Json(new { Result = true, Text = "修改成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Text = ex.Message });
            }
        }

        //
        // POST: /Base/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {
                dao.DeleteObject(dao.GetEntityByKey(id));
                return Content("删除成功");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        

        #region 权限

        /// <summary>
        /// 微软设计这个无参的构造的Controller 有利于使用IOC容器提高对象的创建效率
        /// 如果设计了System.Web.Routing.RequestContext参数，由于每次来的RequestContext都不相同
        /// 则Controller 就要不停的动态创建
        /// </summary>
        public BaseController()
        {
            //无参的构造
        }

        /// <summary>
        /// 改造一个构造函数切入点
        /// 这种方式虽然使得切入机会早，并且可以较早的构造中对业务层注入一些用户信息。
        /// 但是缺点就是每次都要动态反射(因为每次来的HttpContext请求都不相同）
        /// </summary>
        /// <param name="requestContext"></param>
        public BaseController(System.Web.Routing.RequestContext requestContext)
        {
            this.OnInit(requestContext);  //这样可以在构造的时候就切入了
        }

        /// <summary>
        /// 比较早的切入点 在ControllerFactory被创建的时候顺便就实现权限验证
        /// </summary>
        /// <param name="requestContext"></param>
        public virtual void OnInit(System.Web.Routing.RequestContext requestContext)
        {
            //这里实现用户信息的相关验证业务
            //if (requestContext.HttpContext.Session["UserState"] != null)
            //{
            //    User userState = requestContext.HttpContext.Session["UserState"] as User;

            //    string controllerName = requestContext.RouteData.Values["controller"].ToString() + "Controller";
            //    string actionName = requestContext.RouteData.Values["action"].ToString();

            //    //判断有没有Action操作权限
            //    //userState.AuthCollection.Contains(controllerName + "/" + acitonName);
            //}
            //else
            //{
            //    //非登录用户跳转
            //    requestContext.HttpContext.Response.Redirect("/User/Login");
            //}
        }

        /// <summary>
        /// 比较晚的切入点 IController在执行Execute之后，Action被执行之前使用的
        /// </summary>
        public virtual void OnInit()
        {
            //这里实现用户信息的相关验证业务
            //if (this.HttpContext.Session["UserState"] != null)
            //{
            //    User userState = this.HttpContext.Session["UserState"] as User;
            //    string passCode = this.HttpContext.Request.Cookies["UserState"].Value.Trim();

            //    string controllerName = this.RouteData.Values["controller"].ToString() + "Controller";
            //    string actionName = this.RouteData.Values["action"].ToString();

            //    //实现Action操作权限验证业务
            //    //userState.AuthCollection.Contains(controllerName + "/" + acitonName);
            //}
            //else
            //{
            //    //非登录用户跳转
            //    HttpContext.Response.Redirect("/User/Login");
            //}
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.OnInit();  //---------------------------------------------切入点 
        }

        //除上述的方式以下方式 
        //我们还可以使用IActionFilter, IAuthorizationFilter标签属性的方式实现权限验证 （这个不在本次讨研究范围内)
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
        //    object[] controllerFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginLessAttribute), false);

        //    CookieHelper cookieHelper = new CookieHelper();
        //    string userstr = cookieHelper.GetCookie("User");
        //    if (userstr != null)
        //    {
        //        IUserState _userState = JSON.Instance.ToObject(userstr) as UserAuthModule;

        //        //这里实现用户信息的相关验证业务
        //        if (_userState != null && _userState.UserID > 0)
        //        {
        //            UserState = _userState;
        //            ViewData["UserState"] = UserState;

        //            //判断有没有Action操作权限
        //            //userState.AuthCollection.Contains(controllerName + "/" + acitonName);
        //        }
        //        else if (controllerFilter.Length == 0)
        //        {
        //            String controller = this.ControllerContext.RouteData.Values["controller"].ToString();
        //            String action = this.ControllerContext.RouteData.Values["action"].ToString();
        //            //非登录用户跳转
        //            Response.Redirect("/User/Login" + (string.IsNullOrEmpty(action) ? "" : "?c=" + controller + "&a=" + action));
        //        }
        //    }
        //    else if (controllerFilter.Length == 0)
        //    {
        //        String controller = this.ControllerContext.RouteData.Values["controller"].ToString();
        //        String action = this.ControllerContext.RouteData.Values["action"].ToString();
        //        //非登录用户跳转
        //        Response.Redirect("/User/Login" + (string.IsNullOrEmpty(action) ? "" : "?c=" + controller + "&a=" + action));
        //    }
            base.OnActionExecuting(filterContext);
            //this.OnInit();//---------------------------------------------切入点
        }

        protected override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            return;

            object[] controllerFilter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginLessAttribute), false);

            CookieHelper cookieHelper = new CookieHelper();
            string userstr = cookieHelper.GetCookie("User");
            if (userstr != null)
            {
                IUserState _userState = JSON.Instance.ToObject(userstr) as UserAuthModule;

                //这里实现用户信息的相关验证业务
                if (_userState != null && _userState.UserID > 0)
                {
                    UserState = _userState;
                    ViewData["UserState"] = UserState;

                    //判断有没有Action操作权限
                    //userState.AuthCollection.Contains(controllerName + "/" + acitonName);
                }
                else if (controllerFilter.Length == 0)
                {
                    String controller = this.ControllerContext.RouteData.Values["controller"].ToString();
                    String action = this.ControllerContext.RouteData.Values["action"].ToString();
                    //非登录用户跳转
                    Response.Redirect("/User/Login" + (string.IsNullOrEmpty(action) ? "" : "?c=" + controller + "&a=" + action));
                }
            }
            else if (controllerFilter.Length == 0)
            {
                String controller = this.ControllerContext.RouteData.Values["controller"].ToString();
                String action = this.ControllerContext.RouteData.Values["action"].ToString();
                //非登录用户跳转
                Response.Redirect("/User/Login" + (string.IsNullOrEmpty(action) ? "" : "?c=" + controller + "&a=" + action));
            }
            
            //this.OnInit();//---------------------------------------------切入点
        }


        #endregion
    }
}
