using dal.ef.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers.PRD
{
    public class ProductController : Controller
    {
        [Inject]
        public IProductRepository productRepository { get; set; }

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(productRepository.GetAll());
        }


    }
}
