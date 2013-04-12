using System;
using System.Data;
using System.Web.Mvc;
using MvcAjaxForm.Models;
using beango.dal;
using beango.model;

namespace MvcAjaxForm.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        //
        // GET: /Base/
        public virtual ActionResult List()
        {
            NorthwindContext entities = new NorthwindContext();
            return View();
        }

        //
        // GET: /Base/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
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
                NorthwindContext entities = new NorthwindContext();
                //entities.AddObject(typeof(T).Name,obj);
                entities.SaveChanges();

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
            return View();
        }

        //
        // POST: /Base/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
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
        // GET: /Base/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Base/Delete/5

        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
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
