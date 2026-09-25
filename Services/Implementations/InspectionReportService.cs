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
using Newtonsoft.Json;

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
            //Get Inspection Report Data.
            InspectionReport report =
                await GetReportDataAsync(id);

            if (report == null)
            {
                return null;
            }
            //Fetch Data tables
            MPIReportDataSet reportData =
                new MPIReportDataSet();

            
           
            reportData.Build(report);
            //Build is Populating the Data Correctly.
           // string json = JsonConvert.SerializeObject(
           //    reportData.DataSet,
           //    Formatting.Indented
           //);

           // System.Diagnostics.Debug.WriteLine("Report Data", json);
            string reportPath =
                HostingEnvironment.MapPath(
                    "~/Reports/InspectionReport2.rpt");


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
                reportDocument.SetDataSource(reportData.DataSet);

                foreach (CrystalDecisions.CrystalReports.Engine.Table table
                         in reportDocument.Database.Tables)
                {
                    //System.Diagnostics.Debug.WriteLine(
                    //    "Table Name: " + table.Name);

                    //System.Diagnostics.Debug.WriteLine(
                    //    "Location: " + table.Location);
                }

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