using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using MvcAjaxForm.Models;
using beango.dal;
using beango.model;

namespace MvcAjaxForm.Controllers.Product
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult List()
        {
            NorthwindContext entities = new NorthwindContext();
            return View(entities.Products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id)
        {
            NorthwindContext entities = new NorthwindContext();
            var p = entities.Products.FirstOrDefault(item => item.ProductID == id);
            return View(p);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Products product)
        {
            try
            {
                // TODO: Add update logic here
                NorthwindContext entities = new NorthwindContext();

                //entities.Products.Attach(product);
                //var entry = entities.ObjectStateManager.GetObjectStateEntry(product);
                //entry.SetModifiedProperty("ProductName"); entry.SetModifiedProperty("Discontinued");
                //entities.SaveChanges();

                string resultText = "修改成功：" + product.ProductID.ToString();
                return Json(new { Result = true, Text = resultText });
            }
            catch (Exception exception)
            {
                return Json(new { Result = false, Text = exception.Message });
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Product/Delete/5

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
