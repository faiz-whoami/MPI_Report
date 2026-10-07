using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPlatformService _platformService;

        public HomeController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return Redirect(Url.Content("~/index.html"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectJob(int jobId)
        {
            Job job = _platformService.GetJobById(jobId);
            if (job == null)
            {
                Response.StatusCode = 404;
                return Json(new { message = "Job was not found." });
            }

            JobContext.Set(Session, job.JobId, job.JobNo, job.CustomerId, job.RigId);
            return Json(new { success = true });
        }
    }
}
