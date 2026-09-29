using System.Collections.Generic;
using System.Web.Mvc;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IPlatformService _platformService;

        public InventoryController(IPlatformService platformService)
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

            IList<InventoryItem> items = _platformService.GetInventory(JobContext.GetJobId(Session).Value);
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

            return View(new InventoryItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryItem model)
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

            _platformService.AddInventory(model);
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
