using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class CorrectiveActionController : Controller
    {
        private readonly IPlatformService _platformService;

        public CorrectiveActionController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        public ActionResult Index()
        {
            return Redirect(Url.Content("~/index.html") + "#/corrective-actions");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Redirect(Url.Content("~/index.html") + "#/corrective-actions/create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CorrectiveAction model)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return Failure("Select a client / rig / job first.", 409);
            }

            model.JobId = jobId.Value;
            if (string.IsNullOrWhiteSpace(model.Status))
            {
                model.Status = "Open";
            }

            if (!ModelState.IsValid)
            {
                return Failure("Please correct the submitted corrective action.", 400);
            }

            _platformService.AddCorrectiveAction(model);
            return Json(new { success = true });
        }

        private ActionResult Failure(string message, int statusCode)
        {
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            return Json(new { message });
        }
    }
}
