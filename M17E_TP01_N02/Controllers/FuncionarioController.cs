using M17E_TP01_N02.Models;
using System.Web;
using System.Web.Mvc;

namespace M17E_TP01_N02.Controllers {
    public class FuncionarioController : Controller {
        DbFuncionarios _bd = new DbFuncionarios();
        // GET: Funcionario
        public ActionResult Index() {
            return View(_bd.ListAllActive());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionariosModel dados, HttpPostedFileBase fotografia) {
            if (!ModelState.IsValid) return View(dados);
            if (fotografia == null) {
                ModelState.AddModelError("", "Indique uma fotografia para o funcionário");
                return View(dados);
            }
            var id = _bd.CreateFuncionario(dados);

            var caminho = Server.MapPath("~/Content/Images/Funcionarios") + id + ".jpg";
            fotografia.SaveAs(caminho);

            return RedirectToAction("index", "home");
        }

        public ActionResult Edit(int? id) {
            if (id == null) return RedirectToAction("index");
            return View(_bd.UserInfo((int)id)[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionariosModel dados) {
            if (ModelState.IsValid) {
                _bd.AtualizarCliente(dados);
                return RedirectToAction("index");
            }
            return View(dados);
        }
    }
}