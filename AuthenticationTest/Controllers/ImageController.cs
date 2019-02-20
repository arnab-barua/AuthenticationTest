using System.Web;
using System.Web.Mvc;

namespace AuthenticationTest.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase logo)
        {
            var img = System.Drawing.Image.FromStream(logo.InputStream, true, true);
            int w = img.Width;
            int h = img.Height;
            return View();
        }
    }
}