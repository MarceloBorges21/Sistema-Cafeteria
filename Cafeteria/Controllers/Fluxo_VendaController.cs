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
    public class Fluxo_VendaController : BaseController
    {
        Fluxo_VendaDAO DAO = new Fluxo_VendaDAO();
        // GET: Fluxo_Venda
           
                // GET: Gerenciamento/Setor
                public ActionResult Index()
                {
                    var lista = DAO.Get().ToList();

                    ViewBag.ListaFluxo_Venda = lista;

                    return View();
                }

                public JsonResult AbreVenda(int mesa)
                {
                    DAO.AbrirVenda(mesa);
                    return Json("Sucesso!");
                }

                public JsonResult SalvarItens(int Id_Venda, int Id_Produto, int Quantidade)
                {
                      DAO.SalvarItens(Id_Venda, Id_Produto, Quantidade);
                      return Json("Sucesso!");
                }

                //carrega comanda
                public JsonResult CarregaTodaComanda(int Id_Venda)
                {
                    var lista = DAO.CarregaTodaComanda(Id_Venda).ToList();
                    return Json(lista, JsonRequestBehavior.AllowGet);
                }

                //Mudar Status
                [HttpPost]
                public JsonResult FecharComanda(int Id_Venda, string FormaPagamento)
                {   //Fecha a comanda
                    DAO.EditarStatus(Id_Venda, FormaPagamento);

                    //Pega valor total
                    decimal total = DAO.PegaValorTotal(Id_Venda);

                     //salva total no fluxo
                     DAO.SalvarTotalConta(Id_Venda, total);

                    return Json("Sucesso!");
                }
    }
}

