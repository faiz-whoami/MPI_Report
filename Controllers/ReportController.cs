using MPI_Report.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPI_Report.Controllers
{
    public class ReportController : Controller
    {
        private readonly IInspectionReportService _reportService;

        public ReportController(
            IInspectionReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult> MPI(int id)
        {
            byte[] pdf =
                await _reportService.GeneratePdfAsync(id);

            if (pdf == null)
            {
                return HttpNotFound(
                    "Inspection report was not found.");
            }

            return File(
                pdf,
                "application/pdf",
                "MPIInspectionReport.pdf");
        }
    }
}