using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MPI_Report.Infrastructure;

namespace MPI_Report.Controllers
{
    [Authorize]
    public abstract class AdminApiController : Controller
    {
        protected ActionResult ApiOk(object data)
        {
            return JsonApiResult.Ok(data);
        }

        protected ActionResult ApiFail(string error, object data = null)
        {
            return JsonApiResult.Fail(error, data);
        }

        protected ActionResult ApiFail(IList<string> errors, object data = null)
        {
            return JsonApiResult.Fail(errors, data);
        }

        protected ActionResult ApiFail(ModelStateDictionary modelState, object data = null)
        {
            IList<string> errors = modelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                    ? "The request is invalid."
                    : error.ErrorMessage)
                .Distinct()
                .ToList();

            if (errors.Count == 0)
            {
                errors.Add("The request is invalid.");
            }

            return ApiFail(errors, data);
        }

        protected ActionResult NeedsJob()
        {
            return ApiFail(
                "Select a client / rig / job first.",
                new { NeedsJob = true });
        }
    }
}
