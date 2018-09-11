using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cafeteria.App_Start
{
    public class BaseController : Controller
    {
        protected string RedirectPath = string.Empty;
        protected bool DoRedirect = false;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string sessao = Convert.ToString(Session["login"]);
            if (sessao == "")
            {
                DoRedirect = true;
                RedirectPath = Url.Action("Index", "Home");
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string sessao = Convert.ToString(Session["login"]);

            if (sessao != "")
            {
                base.OnActionExecuted(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}