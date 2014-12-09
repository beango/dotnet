using System.Web.Mvc;

namespace beango.web.Areas.BAS
{
    public class BASAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BAS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BAS_default",
                "BAS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "beango.web.Areas.controller.BAS" }
            );
        }
    }
}
