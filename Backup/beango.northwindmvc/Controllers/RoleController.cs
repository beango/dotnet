using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using beango.dal;
using beango.model;

namespace beango.northwindmvc.Controllers
{
    public class RoleController : BaseController<Role>
    {
        public RoleController(IDao<Role> dao)
            : base(dao)
        {

        }

        public ActionResult RenderRoles(string name, string selected)
        {
            StringBuilder s =
                new StringBuilder(string.Format("<select{0}>",
                                                string.IsNullOrEmpty(name) ? "" : " name=\"" + name + "\""));
            var roles = new DaoTemplate<Role>().FindList();
            if (roles != null)
            {
                foreach (Role role in roles)
                    s.AppendFormat("<option value=\"{0}\">{1}</option>",
                                   role.RoleID.ToString() +
                                   (selected == role.RoleID.ToString() ? " selected=\"true\"" : ""), role.RoleName);
            }
            s.Append("</select>");
            return Content(s.ToString());
        }

        public ActionResult RoleAuth(int id)
        {
            var auths = new DaoTemplate<Auth>().FindList();
            var roleauths = new DaoTemplate<RoleAuth>().FindList(obj => obj.RoleID == id);

            TempData["auths"] = auths;
            TempData["role"] = new DaoTemplate<Role>().GetEntityByKey(id);
            return View(roleauths);
        }

        [HttpPost]
        public ActionResult RoleAuthSubmit(FormCollection formCollection)
        {
            int roleid = Convert.ToInt32(formCollection["roleid"]);
            var auths = new DaoTemplate<Auth>().FindList();
            var exists = new DaoTemplate<RoleAuth>().FindList(obj => obj.RoleID == roleid);
            List<RoleAuth> addroleauths = new List<RoleAuth>(),
                           delroleauths = new List<RoleAuth>();
            if (auths != null)
            {
                foreach (var auth in auths)
                {
                    var formitem = formCollection["chkChecked" + auth.AuthID.ToString()];
                    if (null != formitem)
                    {
                        if (formitem.Contains("true"))
                        {
                            if (exists.All(obj => obj.AuthID != auth.AuthID)) //原权限中不存在
                                addroleauths.Add(new RoleAuth() {RoleID = roleid, AuthID = auth.AuthID});
                        }
                        else
                        {
                            var e = exists.FirstOrDefault(obj => obj.AuthID == auth.AuthID);
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
                new DaoTemplate<RoleAuth>().AddObject(addroleauths);
            }
            if (delroleauths.Count > 0)
            {
                new DaoTemplate<RoleAuth>().DeleteObject(delroleauths);
            }
            return Redirect("/Role/RoleAuth/" + roleid.ToString());
        }
    }
}
