using M17E_TP01_N02.Models;
using System.Web;
using System.Web.Mvc;

namespace M17E_TP01_N02.Controllers
{
    public class FuncionarioController : Controller
    {
        DbFuncionarios bd = new DbFuncionarios();
        // GET: Funcionario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionariosModel dados, HttpPostedFileBase fotografia)
        {
            if (ModelState.IsValid)
            {
                if (fotografia == null)
                {
                    ModelState.AddModelError("", "Indique uma fotografia para o funcionário");
                    return View(dados);
                }
                int id = bd.CreateFuncionario(dados);

                string caminho = Server.MapPath("~/Content/Images/Funcionarios") + id + ".jpg";
                fotografia.SaveAs(caminho);

                return RedirectToAction("index","home");
            }
            return View(dados);
        }
    }
}