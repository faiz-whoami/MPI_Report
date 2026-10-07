using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;
using MPI_Report.ViewModels;

namespace MPI_Report.Controllers
{
    [Authorize]
    public class FrontendApiController : Controller
    {
        private readonly IPlatformService _platformService;
        private readonly ICustomerService _customerService;
        private readonly IInspectionReportService _reportService;
        private readonly IAccountService _accountService;

        public FrontendApiController(
            IPlatformService platformService,
            ICustomerService customerService,
            IInspectionReportService reportService,
            IAccountService accountService)
        {
            _platformService = platformService;
            _customerService = customerService;
            _reportService = reportService;
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            int? jobId = JobContext.GetJobId(Session);
            DashboardCounts counts = _platformService.GetDashboardCounts(jobId);

            return Json(new
            {
                selectedJobId = jobId,
                selectedJobNo = JobContext.GetJobNo(Session),
                jobs = _platformService.GetActiveJobs().Select(job => new
                {
                    job.JobId,
                    job.JobNo,
                    RigName = job.Rig == null ? string.Empty : job.Rig.Name,
                    CustomerName = job.Customer == null ? string.Empty : job.Customer.Name
                }),
                counts.InventoryCount,
                counts.OpenCorrectiveActionCount,
                counts.DailyMeetingCount,
                counts.ChecklistCount,
                counts.MpiReportCount
            }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AntiForgeryToken()
        {
            string oldCookieToken = Request.Cookies[AntiForgeryConfig.CookieName]?.Value;
            string newCookieToken;
            string formToken;
            AntiForgery.GetTokens(oldCookieToken, out newCookieToken, out formToken);

            if (!string.IsNullOrEmpty(newCookieToken))
            {
                Response.Cookies.Set(new HttpCookie(AntiForgeryConfig.CookieName, newCookieToken)
                {
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection,
                    Path = Request.ApplicationPath
                });
            }

            return Json(new { token = formToken }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Failure("Enter a valid username and password.", 400);
            }

            User user = _accountService.Validate(model.Username, model.Password);
            if (user == null)
            {
                return Failure("Invalid username or password.", 400);
            }

            FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);
            return Json(new
            {
                success = true,
                returnUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                    ? returnUrl
                    : Url.Content("~/index.html")
            });
        }

        [HttpGet]
        public ActionResult SessionInfo()
        {
            return Json(new { username = User.Identity.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            JobContext.Clear(Session);
            Session.Abandon();
            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectJob(int jobId)
        {
            Job job = _platformService.GetJobById(jobId);
            if (job == null)
            {
                return Failure("Job was not found.", 404);
            }

            JobContext.Set(Session, job.JobId, job.JobNo, job.CustomerId, job.RigId);
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            return Json(_platformService.GetInventory(jobId.Value).Select(item => new
            {
                item.InventoryItemId,
                item.ItemCode,
                item.Description,
                item.Position,
                item.ItemType,
                item.Status,
                item.SpecialType
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInventory(InventoryItem model)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            model.JobId = jobId.Value;
            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            _platformService.AddInventory(model);
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Checklists()
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            return Json(_platformService.GetChecklists(jobId.Value).Select(item => new
            {
                item.ChecklistId,
                item.Title,
                item.Frequency,
                ChecklistDate = item.ChecklistDate.ToString("o"),
                itemCount = item.Items.Count
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChecklist(InspectionChecklist model, string itemLines)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            model.JobId = jobId.Value;
            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            if (!string.IsNullOrWhiteSpace(itemLines))
            {
                string[] lines = itemLines.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    model.Items.Add(new ChecklistItem { Description = line.Trim() });
                }
            }

            _platformService.AddChecklist(model);
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult CorrectiveActions()
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            return Json(_platformService.GetCorrectiveActions(jobId.Value).Select(item => new
            {
                item.CorrectiveActionId,
                item.Title,
                item.Criticality,
                item.Status,
                DueDate = item.DueDate.HasValue ? item.DueDate.Value.ToString("o") : null
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCorrectiveAction(CorrectiveAction model)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            model.JobId = jobId.Value;
            if (string.IsNullOrWhiteSpace(model.Status))
            {
                model.Status = "Open";
            }

            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            _platformService.AddCorrectiveAction(model);
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult DailyMeetings()
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            return Json(_platformService.GetDailyMeetings(jobId.Value).Select(item => new
            {
                item.DailyMeetingId,
                MeetingDate = item.MeetingDate.ToString("o"),
                item.Description,
                item.Signatures
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDailyMeeting(DailyMeeting model)
        {
            int? jobId = JobContext.GetJobId(Session);
            if (!jobId.HasValue)
            {
                return NoJob();
            }

            model.JobId = jobId.Value;
            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            _platformService.AddDailyMeeting(model);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<ActionResult> Customers(string search, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                status = "active";
            }

            var customers = await _customerService.SearchAsync(search, status);
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Customer(int id)
        {
            CustomerFormViewModel customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return Failure("Customer was not found.", 404, allowGet: true);
            }

            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            int customerId = await _customerService.CreateAsync(model);
            if (customerId <= 0)
            {
                return Failure("Unable to create the customer.", 400);
            }

            return Json(new { success = true, customerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCustomer(CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationFailure();
            }

            bool updated = await _customerService.UpdateAsync(model);
            if (!updated)
            {
                return Failure("Customer was not found.", 404);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            bool deleted = await _customerService.DeleteAsync(id);
            if (!deleted)
            {
                return Failure("Customer was not found.", 404);
            }

            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Reports()
        {
            return Json(_reportService.GetAllReports().Select(report => new
            {
                report.InspectionReportId,
                report.ReportNo,
                report.WorkOrderNo,
                customerName = report.Customer == null ? string.Empty : report.Customer.Name,
                report.TestLocation,
                InspectionDate = report.InspectionDate.ToString("o"),
                RecommendedDueDate = report.RecommendedDueDate.HasValue ? report.RecommendedDueDate.Value.ToString("o") : null,
                report.InspectionResult
            }), JsonRequestBehavior.AllowGet);
        }

        private ActionResult NoJob()
        {
            return Failure("Select a client / rig / job first.", 409, allowGet: true);
        }

        private ActionResult ValidationFailure()
        {
            var errors = ModelState
                .Where(entry => entry.Value.Errors.Count > 0)
                .ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value.Errors.Select(error =>
                        string.IsNullOrWhiteSpace(error.ErrorMessage)
                            ? "The submitted value is invalid."
                            : error.ErrorMessage));

            return Failure("Please correct the highlighted fields.", 400, errors);
        }

        private ActionResult Failure(string message, int statusCode, object errors = null, bool allowGet = false)
        {
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            return Json(
                new { message, errors },
                allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet);
        }
    }
}