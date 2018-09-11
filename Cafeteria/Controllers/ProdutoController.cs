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
    public class ProdutoController : BaseController
    {
		ProdutoDAO DAO = new ProdutoDAO();
		// GET: Gerenciamento/Setor
		public ActionResult Index()
		{
			var lista = DAO.Listar().ToList();

			ViewBag.ListaProduto = lista;

			return View();
		}
        public ActionResult Cadastro(int? Id)
        {
            if (Id == null)
            {
                //se for null é cadastro novo, preenche vazio
                Id = 0;
                ViewBag.Id = Id;
                ViewBag.Descricao = "";
                ViewBag.Valor = "";
                ViewBag.Data = "";
            }
            else
            {
                //preenche os campos com dados do banco de dados
                
                ViewBag.Id = Id; //recebe o id do parametro

                var lista = DAO.Get(Convert.ToInt32(Id));
                ViewBag.ListaCadastro = lista.ToList();

                foreach (var item in ViewBag.ListaCadastro)
                {
                    ViewBag.Descricao = item.Descricao;
                    ViewBag.Valor = item.Valor.ToString(CultureInfo.CreateSpecificCulture("en-US"));
                    ViewBag.Data = item.Data;
                }
            }
            
            return View();

        }
        public ActionResult Salvar(Produto u)
        {
            if (u.Id > 0 )
            {
                DAO.Editar(u.Id,u.Valor);
            }
            else
            {
                DAO.SalvarPeloObjeto(u);
            }
            
            return RedirectToAction("Index", "Produto");
        }
        public ActionResult Excluir (int Id)
        {
            DAO.Excluir(Id);
            return RedirectToAction("Index", "Produto");
        }

        public JsonResult DropSelecionarProduto()
        {
            var lista = DAO.Listar().ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
         }

        public JsonResult CarregaDados(int Id)
        {
            var lista = DAO.Get(Id);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarAjax(Produto u)
        {
            DAO.SalvarPeloObjeto(u);
            return Json("Dados salvos com sucesso.");
        }

        [HttpPost]
        public JsonResult EditarAjax(int Id, string Valor)
        {
            DAO.Editar(Id,Valor);
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