using System.Collections.Generic;
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
            ActionResult redirect = RedirectIfNoJob();
            if (redirect != null)
            {
                return redirect;
            }

            IList<CorrectiveAction> items = _platformService.GetCorrectiveActions(JobContext.GetJobId(Session).Value);
            return View(items);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ActionResult redirect = RedirectIfNoJob();
            if (redirect != null)
            {
                return redirect;
            }

            CorrectiveAction model = new CorrectiveAction();
            model.Status = "Open";
            model.Criticality = "Minor";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CorrectiveAction model)
        {
            ActionResult redirect = RedirectIfNoJob();
            if (redirect != null)
            {
                return redirect;
            }

            model.JobId = JobContext.GetJobId(Session).Value;
            if (string.IsNullOrWhiteSpace(model.Status))
            {
                model.Status = "Open";
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _platformService.AddCorrectiveAction(model);
            return RedirectToAction("Index");
        }

        private ActionResult RedirectIfNoJob()
        {
            if (!JobContext.GetJobId(Session).HasValue)
            {
                TempData["Message"] = "Select a client / rig / job first.";
                return RedirectToAction("Index", "Home");
            }

            return null;
        }
    }
}
