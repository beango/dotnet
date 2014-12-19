using dal;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace web.core.Controllers
{
    //====================================================================== 
    // create by:hd
    // create date:2014-12-18 11:03
    // filename :Q-PC
    // description : 产品分类
    // 
    //======================================================================
    public class BaseController<T> : Controller where T : class
    {
        [Inject]
        public IUnitOfWork unitOfWork { get; set; }
    }
}
