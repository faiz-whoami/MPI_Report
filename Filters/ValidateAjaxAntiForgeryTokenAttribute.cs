using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MPI_Report.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ValidateAjaxAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string headerToken = request.Headers["RequestVerificationToken"];
            if (string.IsNullOrWhiteSpace(headerToken))
            {
                headerToken = request.Headers["X-Request-Verification-Token"];
            }

            if (!string.IsNullOrWhiteSpace(headerToken))
            {
                HttpCookie cookie = request.Cookies[AntiForgeryConfig.CookieName];
                AntiForgery.Validate(cookie == null ? null : cookie.Value, headerToken);
                return;
            }

            new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
        }
    }
}
