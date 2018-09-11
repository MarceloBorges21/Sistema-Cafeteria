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
    public class Fluxo_ItemController : BaseController
	{
		Fluxo_ItemDAO DAO = new Fluxo_ItemDAO();
		
				// GET: Gerenciamento/Setor
				public ActionResult Index()
				{
					var lista = DAO.Listar().ToList();

					ViewBag.ListaFluxo_Item = lista;

					return View();
				}
								
                [HttpPost]
                public JsonResult SalvarAjax(Fluxo_Item u)
                {
                    DAO.SalvarPeloObjeto(u);
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