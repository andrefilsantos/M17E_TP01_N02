using System.Web.Mvc;
using M17E_TP01_N02.Models;

namespace M17E_TP01_N02.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly DbMaquinas _bd = new DbMaquinas();
        // GET: Maquinas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaquinasModel dados) {
            if (!ModelState.IsValid) return View(dados);
            if (_bd.AdicionarMaquina(dados))
                RedirectToAction("Index");
            return View(dados);
        }

        public ActionResult Edit(int? id) {
            if (id == null) return RedirectToAction("Index");
            return View(_bd.Lista((int)id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaquinasModel dados) {
            if (ModelState.IsValid) {
                _bd.AtualizarMaquina(dados);
                return RedirectToAction("Index");
            }
            return View(dados);
        }

        public ActionResult Delete(int? id) {
            if (id == null) return RedirectToAction("Index");
            return View(_bd.Lista((int)id)[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmarDelete(int? id) {
            if (id != null) _bd.RemoverMaquina((int)id);
            return RedirectToAction("Index");
        }
    }
}