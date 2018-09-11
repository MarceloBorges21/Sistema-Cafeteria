using Cafeteria.App_Start;
using Cafeteria.Models.DAO;
using Cafeteria.Models.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeteria.Controllers
{
    public class FuncionarioController : BaseController
    {
        FuncionarioDAO DAO = new FuncionarioDAO();
        // GET: Gerenciamento/Setor
        public ActionResult Index()
        {
            var lista = DAO.Listar().ToList();

            ViewBag.ListaFuncionario = lista;

            return View();
        }
        public ActionResult Cadastro(int? Id)
        {
            if (Id == null)
            {
                //se for null é cadastro novo, preenche vazio
                Id = 0;
                ViewBag.Id = Id;
                ViewBag.Nome = "";
                ViewBag.Endereco = "";
                ViewBag.Login = "";
                ViewBag.Senha = "";
            }
            else
            {
                //preenche os campos com dados do banco de dados

                ViewBag.Id = Id; //recebe o id do parametro

                var lista = DAO.Get(Convert.ToInt32(Id));
                ViewBag.ListaCadastro = lista.ToList();

                foreach (var item in ViewBag.ListaCadastro)
                {
                    ViewBag.Nome = item.Nome;
                    ViewBag.Endereco = item.Endereco;
                    ViewBag.Login = item.Login;
                    ViewBag.Senha = item.Senha;
                }
            }

            return View();

        }
        public ActionResult Salvar(Funcionario u)
        {
            if (u.Id > 0)
            {
                DAO.Editar(u.Id, u.Endereco, u.Login , u.Senha);
            }
            else
            {
                DAO.SalvarPeloObjeto(u);
            }

            return RedirectToAction("Index", "Funcionario");
        }

        public ActionResult Excluir(int Id)
        {
            DAO.Excluir(Id);
            return RedirectToAction("Index", "Funcionario");
        }

        public JsonResult CarregaDados(int Id)
        {
            var lista = DAO.Get(Id);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarAjax(Funcionario u)
        {
            DAO.SalvarPeloObjeto(u);
            return Json("Dados salvos com sucesso.");
        }

        [HttpPost]
        public JsonResult EditarAjax(int Id, string Endereco, string Login, string Senha)
        {
            DAO.Editar(Id, Endereco, Login, Senha);
            return Json("Dados salvos com sucesso.");
        }

        [HttpPost]
        public JsonResult ExcluirAjax(int Id)
        {
            DAO.Excluir(Id);
            return Json("Dados exluido com sucesso.");
        }

    }
}