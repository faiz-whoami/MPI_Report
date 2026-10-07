using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;
using MPI_Report.ViewModels;

namespace MPI_Report.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Content("~/index.html") + "#/dashboard");
            }

            string target = Url.Content("~/index.html") + "#/login";
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                target += "?returnUrl=" + HttpUtility.UrlEncode(returnUrl);
            }

            return Redirect(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                Response.StatusCode = 400;
                return Json(new { message = "Enter a valid username and password." });
            }

            User user = _accountService.Validate(model.Username, model.Password);
            if (user == null)
            {
                Response.StatusCode = 400;
                return Json(new { message = "Invalid username or password." });
            }

            FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Json(new { success = true, returnUrl });
            }

            return Json(new { success = true, returnUrl = Url.Content("~/index.html") });
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            JobContext.Clear(Session);
            Session.Abandon();
            return Json(new { success = true });
        }
    }
}
