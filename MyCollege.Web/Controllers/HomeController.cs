using MyCollege.Data;
using System.Linq;
using System.Web.Mvc;

namespace MyCollege.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Context db = new Context();
            var a = db.Students.Select(c=>c);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}