using M17E_TP01_N02.Models;
using System.Web.Mvc;

namespace M17E_TP01_N02.Controllers {
    public class AssistenciaController : Controller {
        readonly DbAssistencia _bd = new DbAssistencia();
        // GET: Assistencia
        public ActionResult Index() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssistenciaModel dados) {
            if (!ModelState.IsValid) return View(dados);
            if (_bd.AdicionarAssistencia(dados))
                RedirectToAction("Index");
            return View(dados);
        }
    }
}