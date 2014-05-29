using System;
using System.Web.Mvc;
using beango.dal;
using beango.model;
using beango.northwindmvc.Module;

namespace beango.northwindmvc.Controllers
{
    [RoleFilter(1)]
    public class ProductController : BaseController<Products>
    {
        public ProductController(IDao<Products> dao)
            : base(dao)
        {

        }

        [HttpPost]
        public override ActionResult Edit(Products obj)
        {
            try
            {
                new DaoTemplate<Products>().UpdateObject(obj);

                return Json(new { Result = true, Text = "修改成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Text = ex.Message });
            }
        }
    }
}
