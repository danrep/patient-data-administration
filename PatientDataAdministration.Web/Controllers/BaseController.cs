using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext); Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDesc = (ReflectedActionDescriptor)filterContext.ActionDescriptor;

            if (actionDesc.ActionName == "LogIn")
                return;

            if (SecurityModel.IsUserSessionActive)
                return;

            filterContext.Result = RedirectToAction("Index", "Security", new {area = ""});
            SecurityModel.ClearSession();
            return;
        }
    }
}