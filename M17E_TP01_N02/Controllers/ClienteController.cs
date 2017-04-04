using System.Web.Mvc;
using M17E_TP01_N02.Models;

namespace M17E_TP01_N02.Controllers {
    public class ClienteController : Controller {
        readonly DbClientes _bd = new DbClientes();
        // GET: Clientes
        public ActionResult Index() {
            return View(_bd.ListaAllActive());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientesModel dados) {
            if (!ModelState.IsValid) return View(dados);
            if (_bd.AdicionarCliente(dados))
                RedirectToAction("Index");
            return View(dados);
        }

        public ActionResult Edit(int? id) {
            if (id == null) return RedirectToAction("Index");
            return View(_bd.Lista((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientesModel dados) {
            if (ModelState.IsValid) {
                _bd.AtualizarCliente(dados);
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
        public ActionResult ConfirmarDelete(int? id)
        {
            if (id != null) _bd.RemoverCliente((int)id);
            return RedirectToAction("Index");
        }
    }
}