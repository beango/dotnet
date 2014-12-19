using AutoMapper;
using common;
using dal;
using model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using web.core.Models;
using web.core.Repositories;

namespace web.core.Controllers.PRD
{
    //====================================================================== 
    // create by:hd
    // create date:2014-12-18 11:17
    // filename :Q-PC
    // description : 
    // 
    //======================================================================
    [Authorize(Roles = "admin")]
    public class CategoryController : BaseController<Categories>
    {
        [Inject]
        public ICategoryRepository categoryRepository { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var list = categoryRepository.GetAll().ToArray();
                var nwlist = Mapper.Map<Categories[], ICollection<CategoryModel>>(list);
                return View(nwlist);
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
        }

    }
}
