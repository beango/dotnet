using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using beango.model;

namespace MvcAjaxForm.Controllers.Role
{
    public class RoleController : BaseController<beango.model.Roles>
    {
        //
        // GET: /Role/Details/5

        public override ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Role/Create

        public override ActionResult Create()
        {
            return View();
        } 

        
        //
        // GET: /Role/Edit/5

        public override ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public override ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Role/Delete/5

        public override ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Role/Delete/5

        [HttpPost]
        public override ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
