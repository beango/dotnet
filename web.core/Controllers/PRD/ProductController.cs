using AutoMapper;
using common;
using dal;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.core.Models;
using web.core.Repositories;
using web.core.Helper;

namespace web.core.Controllers.PRD
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : BaseController<Products>
    {
        [Inject]
        public IProductRepository productRepository { get; set; }
        [Inject]
        public IRepository<Categories> categoryRepository { get; set; }
        [Inject]
        public ISupplierRepository supplierRepository { get; set; }

        [HttpGet]
        public ActionResult Index(string q, int pageindex = 1)
        {
            try
            {
                if (pageindex < 1)
                    pageindex = 1;
                int total;
                var list = productRepository.GetAll(q, pageindex, 15, out total).ToArray();
                var nwlist = Mapper.Map<Products[], ICollection<ProductsModel>>(list);

                var cateall = categoryRepository.GetAll();
                var suppall = supplierRepository.GetAll();
                foreach (var nw in nwlist)
                {
                    nw.Categories = Mapper.Map<CategoryModel>(cateall.FirstOrDefault(model => model.CategoryID == nw.CategoryID));
                    nw.Suppliers = Mapper.Map<SupplierModel>(suppall.FirstOrDefault(model => model.SupplierID == nw.SupplierID));
                }
                return View(nwlist);
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Details(long productid)
        {
            var product = productRepository.GetById(productid);
            var productModel = Mapper.Map<ProductsModel>(product);
            return View(productModel);
        }

        private void GetCommonDT()
        {
            var categories = categoryRepository.GetAll();
            ViewBag.Categories = categories.ToSelectListItems(-1);

            var Suppliers = supplierRepository.GetAll();
            ViewBag.Suppliers = Suppliers.ToSelectListItems(-1);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GetCommonDT();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int productid)
        {
            GetCommonDT();

            var dbentity = productRepository.GetById(productid);
            var product = Mapper.Map<ProductsModel>(dbentity);
            return View(product);
        }

        [HttpPost]
        public ActionResult Save(ProductsModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "提交失败！");
                GetCommonDT();
                if (model.ProductID == 0)//ADD
                    return View("Create",model);
                else
                    return View("Edit", model);
            }
            if (model.ProductID == 0)//ADD
            {
                var product = Mapper.Map<Products>(model);
                product.CreateTime = DateTime.Now;
                productRepository.Add(product);
            }
            else
            {
                var product = productRepository.GetById(model.ProductID);
                Mapper.Map<ProductsModel, Products>(model, product);
                productRepository.Update(product);
            }
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int productid)
        {
            productRepository.Delete(model => model.ProductID == productid);

            return RedirectToAction("Index");
        }
    }
}
