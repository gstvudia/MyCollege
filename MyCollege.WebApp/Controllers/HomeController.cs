using MyCollege.Data.Migrations;
using System.Web.Mvc;

namespace MyCollege.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DbInitializationHandler.Initialize();
            return View();
        }
    }
}
