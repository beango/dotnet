using beango.dal;
using beango.model;

namespace beango.northwindmvc.Controllers
{
    public class AuthController : BaseController<Auth>
    {
       public AuthController(IDao<Auth> dao)
            : base(dao)
        {

        }
    }
}
