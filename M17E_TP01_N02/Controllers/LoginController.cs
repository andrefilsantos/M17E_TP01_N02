using M17E_TP01_N02.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace M17E_TP01_N02.Controllers
{
    public class LoginController : Controller
    {
        LoginDb _bd = new LoginDb();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel dados)
        {
            if (ModelState.IsValid)
            {
                FuncionariosModel funcionario = _bd.LoginFuncionario(dados);
                ClientesModel cliente = _bd.LoginCliente(dados);

                if (funcionario == null && cliente == null)
                {
                    ModelState.AddModelError("", "Login Inválido. Tente novamente.");
                    return View(dados);
                }
                else
                {
                    if (funcionario == null)
                    {
                        Session["tipo"] = "cliente";
                        Session["idCliente"] = cliente.IdCliente;
                        FormsAuthentication.SetAuthCookie(cliente.IdCliente.ToString(), false);
                    }
                    else
                    {
                        Session["tipo"] = "funcionario";
                        Session["idFuncionario"] = funcionario.IdFuncionario;
                        FormsAuthentication.SetAuthCookie(funcionario.IdFuncionario.ToString(), false);
                    }
                    if (Request.QueryString["ReturnUrl"] == null)
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(Request.QueryString["ReturnUrl"].ToString());
                }
            }
            return View(dados);
        }
    }
}