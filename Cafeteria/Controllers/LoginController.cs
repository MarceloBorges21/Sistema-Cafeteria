using Cafeteria.App_Start;
using Cafeteria.Models.DAO;
using Cafeteria.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeteria.Controllers
{
    public class LoginController : Controller
    {
        LoginDAO DAO = new LoginDAO();

        // GET: Login
        public ActionResult Index()
        {
            //Session["Erro"] = "";
            Session["Menu"] = 0;
            Session["login"] = "";
            return View();
        }

        public ActionResult Logar (LoginSenha u)
        {
            string logou = DAO.Logar(u.login, u.Senha);

            if (String.IsNullOrEmpty(logou))//se for vazio ou em branco, login é invalido
            {
                Session["Erro"] = 0;
                Session["Menu"] = 0;
                return RedirectToAction("Index", "Home");
            }
            else // achou login e senha validos
            {
                Session["Menu"] = 1;
                Session["Erro"] = 1;
                return RedirectToAction("Index", "Home");
            } 
			
        }
		public JsonResult Sair(string login, string Senha)
		{
			DAO.Sair(login, Senha);
			return Json(JsonRequestBehavior.AllowGet);
		}
    }
}