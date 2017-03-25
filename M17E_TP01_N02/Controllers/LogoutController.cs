using System.Web.Mvc;
using System.Web.Security;

namespace M17E_TP01_N02.Controllers
{
    public class LogoutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LoggedOut");
        }

        public ActionResult LoggedOut()
        {
            return View();
        }
    }
}