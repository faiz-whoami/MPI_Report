using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class ChecklistController : Controller
    {
        private readonly IPlatformService _platformService;

        public ChecklistController(IPlatformService platformService)
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

            IList<InspectionChecklist> items = _platformService.GetChecklists(JobContext.GetJobId(Session).Value);
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

            InspectionChecklist model = new InspectionChecklist();
            model.ChecklistDate = DateTime.Now;
            model.Frequency = "Daily";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InspectionChecklist model, string itemLines)
        {
            ActionResult redirect = RedirectIfNoJob();
            if (redirect != null)
            {
                return redirect;
            }

            model.JobId = JobContext.GetJobId(Session).Value;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(itemLines))
            {
                string[] lines = itemLines.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    ChecklistItem item = new ChecklistItem();
                    item.Description = lines[i].Trim();
                    model.Items.Add(item);
                }
            }

            _platformService.AddChecklist(model);
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
