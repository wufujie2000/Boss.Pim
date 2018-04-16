using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Boss.Pim.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : PimControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("swagger");
            //return View();
        }
    }
}