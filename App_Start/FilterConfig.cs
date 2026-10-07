using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MPI_Report
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FrontendApiExceptionFilter());
            filters.Add(new FrontendAuthorizeAttribute());
        }
    }

    public class FrontendApiExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"] as string;
            if (!string.Equals(controller, "FrontendApi", System.StringComparison.OrdinalIgnoreCase) ||
                !(filterContext.Exception is HttpAntiForgeryException))
            {
                return;
            }

            filterContext.HttpContext.Response.StatusCode = 400;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            filterContext.Result = new JsonResult
            {
                Data = new
                {
                    code = "anti_forgery",
                    message = "The request verification token expired. Please retry."
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            filterContext.ExceptionHandled = true;
        }
    }

    public class FrontendAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;
            bool isApiRequest = string.Equals(controller, "FrontendApi", System.StringComparison.OrdinalIgnoreCase) ||
                (string.Equals(controller, "Report", System.StringComparison.OrdinalIgnoreCase) &&
                 string.Equals(action, "MPI", System.StringComparison.OrdinalIgnoreCase));
            if (isApiRequest)
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new JsonResult
                {
                    Data = new { message = "Authentication is required." },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.HttpContext.Response.StatusCode = 401;
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
