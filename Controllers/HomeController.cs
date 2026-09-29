using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;
using MPI_Report.ViewModels;

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

        public ActionResult Index()
        {
            int? jobId = JobContext.GetJobId(Session);
            DashboardCounts counts = _platformService.GetDashboardCounts(jobId);

            DashboardViewModel model = new DashboardViewModel();
            model.SelectedJobId = jobId;
            model.SelectedJobNo = JobContext.GetJobNo(Session);
            model.Jobs = _platformService.GetActiveJobs();
            model.InventoryCount = counts.InventoryCount;
            model.OpenCorrectiveActionCount = counts.OpenCorrectiveActionCount;
            model.DailyMeetingCount = counts.DailyMeetingCount;
            model.ChecklistCount = counts.ChecklistCount;
            model.MpiReportCount = counts.MpiReportCount;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectJob(int jobId)
        {
            Job job = _platformService.GetJobById(jobId);
            if (job == null)
            {
                TempData["Message"] = "Job was not found.";
                return RedirectToAction("Index");
            }

            JobContext.Set(Session, job.JobId, job.JobNo, job.CustomerId, job.RigId);
            return RedirectToAction("Index");
        }
    }
}
