using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
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
            //TODO: Check User Authentication in ASP .NET MVC
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
           
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Values
                            .SelectMany(value => value.Errors)
                            .Select(error => error.ErrorMessage)
                            .ToList()
                    });
                }

                return View(model);
            }

            User user = _accountService.Validate(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");

                if (Request.IsAjaxRequest())
                {
                    return Json(new
                    {
                        success = false,
                        errors = new[] { "Invalid username or password." }
                    });
                }

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);

            var redirectUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? returnUrl
                : Url.Action("Index", "Home");

            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = true,
                    redirectUrl
                });
            }

            return Redirect(redirectUrl);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            JobContext.Clear(Session);
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
