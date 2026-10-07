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
            return Redirect(Url.Content("~/index.html") + "#/checklists");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Redirect(Url.Content("~/index.html") + "#/checklists/create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InspectionChecklist model, string itemLines)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return Failure("Select a client / rig / job first.", 409);
            }

            model.JobId = jobId.Value;
            if (!ModelState.IsValid)
            {
                return Failure("Please correct the submitted checklist.", 400);
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
