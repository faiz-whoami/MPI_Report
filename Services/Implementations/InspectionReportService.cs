using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MPI_Report.Data;
using MPI_Report.Models;
using MPI_Report.Reports.Data;
using MPI_Report.Services.Interfaces;
using System;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MPI_Report.Services.Implementations
{
    public class InspectionReportService : IInspectionReportService
    {
        private readonly ApplicationDbContext _context;

        public InspectionReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<InspectionReport> GetReportDataAsync(int id)
        {
            InspectionReport report =
                await _context.InspectionReports
                    .Include(r => r.Customer)
                    .Include(r => r.InspectionEquipments)
                    .Include(r => r.InspectionConsumables)
                    .Include(r => r.TestEvaluations)
                    .FirstOrDefaultAsync(
                        r => r.InspectionReportId == id);

            return report;
        }

        public async Task<byte[]> GeneratePdfAsync(int id)
        {
            InspectionReport report =
                await GetReportDataAsync(id);

            if (report == null)
            {
                return null;
            }

            MPIReportDataSet reportData =
                new MPIReportDataSet();

            reportData.Build(report);

            string reportPath =
                HostingEnvironment.MapPath(
                    "~/Reports/InspectionReport.rpt");

            if (string.IsNullOrWhiteSpace(reportPath) ||
                !File.Exists(reportPath))
            {
                throw new FileNotFoundException(
                    "Crystal Report file was not found.",
                    reportPath);
            }

            using (ReportDocument reportDocument =
                new ReportDocument())
            {
                reportDocument.Load(reportPath);

                reportDocument.SetDataSource(
                    reportData.DataSet);

                foreach (ReportDocument subReport in reportDocument.Subreports)
                {
                    subReport.SetDataSource(reportData.DataSet);
                }

                using (Stream stream =
                    reportDocument.ExportToStream(
                        ExportFormatType.PortableDocFormat))
                {
                    using (MemoryStream memoryStream =
                        new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);

                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}