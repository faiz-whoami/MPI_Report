using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class DailyMeetingController : Controller
    {
        private readonly IPlatformService _platformService;

        public DailyMeetingController(IPlatformService platformService)
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

            IList<DailyMeeting> items = _platformService.GetDailyMeetings(JobContext.GetJobId(Session).Value);
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

            DailyMeeting model = new DailyMeeting();
            model.MeetingDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DailyMeeting model)
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

            _platformService.AddDailyMeeting(model);
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
