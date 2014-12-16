using AutoMapper;
using beango.util;
using dal.ef.core;
using dal.ef.Repositories;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers.PRD
{
    //[Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        public IUnitOfWork unitOfWork { get; set; }
        public IProductRepository productRepository { get; set; }

        public ProductController(IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }
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

    public class ProductsModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
