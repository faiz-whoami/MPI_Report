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
        public async Task<ActionResult> Index(
            string search,
            string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                status = "active";
            }

            var customers =
                await _customerService.SearchAsync(search, status);

            //var model = new CustomerListViewModel
            //{
            //    Search = search,
            //    Status = status,
            //    Items = customers
            //};

            return View();
        }


        // GET: Customer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customer =
                await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }


        // GET: Customer/Create
        public ActionResult Create()
        {
            var model = new CustomerFormViewModel
            {
                IsActive = true
            };

            return View(model);
        }


        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int customerId =
                await _customerService.CreateAsync(model);

            if (customerId <= 0)
            {
                ModelState.AddModelError(
                    "",
                    "Unable to create the customer.");

                return View(model);
            }

            return RedirectToAction("Index");
        }


        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customer =
                await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }


        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            CustomerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool updated =
                await _customerService.UpdateAsync(model);

            if (!updated)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
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
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
