using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using beango.dal;
using beango.model;
using beango.northwindmvc.Module;

namespace beango.northwindmvc.Controllers
{
    public class DBController : BaseController
    {
        public IDao<Products> dao;
        public DBController(IDao<Products> dao)
        {
            this.dao = dao;
        }

        //
        // GET: /DB/
        [LoginLess]
        public ActionResult Init()
        {
            dao.InitDB();
            return Content("数据初始化完成！");
        }

    }
}
