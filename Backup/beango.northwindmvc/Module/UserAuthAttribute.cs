using System.Linq;
using System.Web.Mvc;
using beango.dal;
using beango.model;
using beango.northwindmvc.Controllers;

namespace beango.northwindmvc.Module
{
    public class RoleFilter : AuthorizeAttribute
    {
        readonly int[] _auths;

        /// <summary>
        /// 角色过滤器构造依法
        /// </summary>
        public RoleFilter(params int[] auths)
        {
            _auths = auths;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var controller = filterContext.Controller as BaseController;
            if (null == controller)
            {
                filterContext.Result = new RedirectResult("/Error/");
            }
            else
            {
                if (controller.UserState == null)
                {
                    filterContext.Result = new RedirectResult("/User/Login");
                }
                else
                {
                    var dao = new DaoTemplate<UserRole>();
                    var rolelist = dao.FindList(obj => obj.UserID == controller.UserState.UserID);
                    if (rolelist != null && rolelist.Count > 0)
                    {
                        foreach (var role in rolelist)
                        {
                           var auths = new DaoTemplate<RoleAuth>().FindList(obj => obj.RoleID == role.RoleID);
                            foreach (var auth in _auths)
                            {
                                if (auths.All(obj => obj.AuthID != auth))
                                {
                                    filterContext.Result = new RedirectResult("/User/NoAuth/" + (dao.FindList().Count));
                                }
                            }
                           
                        }
                    }
                }
            }
        }
    }
}