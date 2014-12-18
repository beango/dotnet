using AutoMapper;
using common;
using dal.ef.core;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.core.Models;
using web.core.Repositories;

namespace web.core.Controllers.PRD
{
    [Authorize(Roles = "admin")]
    public class ProductController : BaseController<Products>
    {
        [Inject]
        public IProductRepository productRepository { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var list = productRepository.GetAll().ToArray();
                var nwlist = Mapper.Map<Products[], ICollection<ProductsModel>>(list);
                return View(nwlist);
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductsModel model)
        {
            return Edit(model);
        }

        [HttpGet]
        public ActionResult Edit(int productid)
        {
            var dbentity = productRepository.GetById(productid);
            var product = Mapper.Map<ProductsModel>(dbentity);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ProductsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ProductID == 0)//ADD
            {
                var product = Mapper.Map<Products>(model);
                productRepository.Add(product);
            }
            else
            {
                var product = productRepository.GetById(model.ProductID);
                Mapper.Map<ProductsModel, Products>(model, product);
                productRepository.Update(product);
            }
            unitOfWork.Commit();
            return Redirect("Index");
        }
    }   
}
