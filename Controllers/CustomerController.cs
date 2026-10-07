using System.Threading.Tasks;
using System.Web.Mvc;
using MPI_Report.Services.Interfaces;
using MPI_Report.ViewModels;

namespace MPI_Report.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer
        public ActionResult Index(
            string search,
            string status)
        {
            return Redirect(Url.Content("~/index.html") + "#/customers");
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return Redirect(Url.Content("~/index.html") + "#/customers/" + id);
        }


        // GET: Customer/Create
        public ActionResult Create()
        {
            return Redirect(Url.Content("~/index.html") + "#/customers/create");
        }


        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failure("Please correct the customer details.", 400);
            }

            int customerId =
                await _customerService.CreateAsync(model);

            if (customerId <= 0)
            {
                return Failure("Unable to create the customer.", 400);
            }

            return Json(new { success = true, customerId });
        }


        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return Redirect(Url.Content("~/index.html") + "#/customers/" + id + "/edit");
        }


        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Failure("Please correct the customer details.", 400);
            }

            bool updated =
                await _customerService.UpdateAsync(model);

            if (!updated)
            {
                return Failure("Customer was not found.", 404);
            }

            return Json(new { success = true });
        }


        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            bool deleted =
                await _customerService.DeleteAsync(id);

            if (!deleted)
            {
                return Failure("Customer was not found.", 404);
            }

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
