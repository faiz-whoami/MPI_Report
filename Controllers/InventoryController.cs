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
            return Redirect(Url.Content("~/index.html") + "#/inventory");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return Redirect(Url.Content("~/index.html") + "#/inventory/create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryItem model)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return Failure("Select a client / rig / job first.", 409);
            }

            model.JobId = jobId.Value;
            if (!ModelState.IsValid)
            {
                return Failure("Please correct the submitted inventory item.", 400);
            }

            _platformService.AddInventory(model);
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
