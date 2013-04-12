using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAjaxForm.Models;
using beango.dal;
using beango.model;

namespace MvcAjaxForm.Controllers.Auth
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult List()
        {
            NorthwindContext entities = new NorthwindContext();
            return View(entities.UserList);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(UserInfo user)
        {
            try
            {
                NorthwindContext entities = new NorthwindContext();

                //entities.UserList.AddObject(user);
                entities.SaveChanges();

                string resultText = "用户添加成功";
                return Json(new { Result = true, Text = resultText });
            }
            catch (Exception exception)
            {
                return Json(new { Result = false, Text = exception.Message });
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
