using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using beango.dal;
using beango.model;

namespace beango.northwindmvc.Controllers
{
    public class UserRoleController : BaseController<UserRole>
    {
        public UserRoleController(IDao<UserRole> dao)
            : base(dao)
        {

        }
        [Inject]
        public IDao<Role> roleDao { get; set; }

        [Inject]
        public IDao<User> ruserDao { get; set; }

        [Inject]
        public IDao<RoleAuth> roleAuthDao { get; set; }
        /// <summary>  
        /// 根据多个条件进行查询  
        /// </summary>  
        /// <param name="userid"></param>
        /// <returns>符合条件的实体的集合</returns>  
        public ActionResult UserRoleList(int userid)
        {
            return View("List",dao.FindList(item => item.UserID == userid));
        }

        public override ActionResult Create()
        {
            int userid = Convert.ToInt32(HttpContext.Request.QueryString["userid"]);
            var user = ruserDao.GetEntity(item => item.UserID == userid);
            if (null != user)
            {
                TempData["UserName"] = user.UserName;
            }
            TempData["roles"] = roleDao.FindList();
            return View();
        }

        [HttpPost]
        public ActionResult UserRoleSubmit(FormCollection formCollection)
        {
            int userid = Convert.ToInt32(formCollection["userid"]);
            var roles = roleDao.FindList();
            var exists = dao.FindList(obj => obj.UserID == userid);
            List<UserRole> addroleauths = new List<UserRole>(),
                           delroleauths = new List<UserRole>();
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    var formitem = formCollection["chkChecked" + role.RoleID.ToString()];
                    if (null != formitem)
                    {
                        if (formitem.Contains("true"))
                        {
                            if (exists.All(obj => obj.RoleID != role.RoleID)) //原权限中不存在
                                addroleauths.Add(new UserRole() { UserID = userid, RoleID = role.RoleID });
                        }
                        else
                        {
                            var e = exists.FirstOrDefault(obj => obj.RoleID == role.RoleID);
                            if (null != e)
                            {
                                delroleauths.Add(e);
                            }
                        }
                    }
                }
            }
            if (addroleauths.Count > 0)
            {
                dao.AddObject(addroleauths);
            }
            if (delroleauths.Count > 0)
            {
                dao.DeleteObject(delroleauths);
            }
            return Json(new { result = true, desc = "/UserRole/UserRoleList?userid=" + userid.ToString() });
        }
    }
}
