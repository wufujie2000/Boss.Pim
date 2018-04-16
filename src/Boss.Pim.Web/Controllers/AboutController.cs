using System.Web.Mvc;

namespace Boss.Pim.Web.Controllers
{
    public class AboutController : PimControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}