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
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace MPI_Report.Services.Implementations
{
    public class InspectionReportService : IInspectionReportService
    {
        private readonly ApplicationDbContext _context;

        public InspectionReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get Report Data USING EF
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

        //GET Report Data USING Stored Procedures.
        private async Task<InspectionReport> GetReportDataFromSpAsync(int id)
        {
            InspectionReport report = null;

            string connectionString = _context.Database.Connection.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("dbo.GetInspectionReport", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ReportId", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    // ==========================================
                    // RESULT SET 1 - REPORT HEADER
                    // ==========================================
                    if (await reader.ReadAsync())
                    {
                        report = new InspectionReport();

                        report.WorkOrderNo = reader["WorkOrderNo"] as string;
                        report.ReportNo = reader["ReportNo"] as string;
                        //report.Customer.Name = reader["CustomerName"] as string;

                        if (reader["InspectionDate"] != DBNull.Value)
                            report.InspectionDate =
                                Convert.ToDateTime(reader["InspectionDate"]);

                        if (reader["RecommendedDueDate"] != DBNull.Value)
                            report.RecommendedDueDate =
                                Convert.ToDateTime(reader["RecommendedDueDate"]);

                        report.TestLocation = reader["TestLocation"] as string;
                        report.ItemDescription = reader["ItemDescription"] as string;
                        report.ItemSerialNo = reader["ItemSerialNo"] as string;
                        report.Manufacturer = reader["Manufacturer"] as string;
                        report.RatingSWL = reader["RatingSWL"] as string;
                        report.ModelNo = reader["ModelNo"] as string;
                        report.ServiceType = reader["ServiceType"] as string;
                        report.InspectionTypeCategory = reader["InspectionTypeCategory"] as string;
                        report.Material = reader["Material"] as string;
                        report.TestMethod = reader["TestMethod"] as string;
                        report.LightingMethod = reader["LightingMethod"] as string;
                        report.MagnetismType = reader["MagnetismType"] as string;
                        report.CurrentType = reader["CurrentType"] as string;
                        report.MagneticFieldDirection = reader["MagneticFieldDirection"] as string;
                        report.ProcedureNo = reader["ProcedureNo"] as string;
                        report.SurfaceCondition = reader["SurfaceCondition"] as string;
                        report.LiftingCapacity = reader["LiftingCapacity"] as string;
                        if (reader["RatingSWL"] != DBNull.Value)
                        {
                            report.RatingSWL = Convert.ToString(reader["RatingSWL"]);
                        }
                        report.Sensitivity = reader["Sensitivity"] as string;
                        report.AcceptanceCriteria = reader["AcceptanceCriteria"] as string;
                        report.LightingType = reader["LightingType"] as string;
                        report.Comments = reader["Comments"] as string;
                        report.AreaOfTesting = reader["AreaOfTesting"] as string;
                        report.RestrictedAccess = reader["RestrictedAccess"] as string;
                        report.InspectionRemarks = reader["InspectionRemarks"] as string;
                        report.InspectionResult = reader["InspectionResult"] as string;
                        report.InspectorName = reader["InspectorName"] as string;
                        report.InspectorQualification = reader["InspectorQualification"] as string;

                        if (reader["InspectorSignedDate"] != DBNull.Value)
                            report.InspectorSignedDate =
                                Convert.ToDateTime(reader["InspectorSignedDate"]);

                        report.ReviewerName = reader["ReviewerName"] as string;
                        report.ReviewerDesignation = reader["ReviewerDesignation"] as string;

                        if (reader["ReviewedDate"] != DBNull.Value)
                            report.ReviewedDate =
                                Convert.ToDateTime(reader["ReviewedDate"]);
                    }

                    if (report == null)
                        return null;


                    // ==========================================
                    // RESULT SET 2 - CONSUMABLES
                    // ==========================================
                    await reader.NextResultAsync();

                    while (await reader.ReadAsync())
                    {
                        InspectionConsumable consumable =
                            new InspectionConsumable();

                        consumable.ConsumableName =
                            reader["ConsumableName"] as string;

                        consumable.BrandType =
                            reader["BrandType"] as string;

                        report.InspectionConsumables.Add(consumable);
                    }


                    // ==========================================
                    // RESULT SET 3 - EQUIPMENTS
                    // ==========================================
                    await reader.NextResultAsync();

                    while (await reader.ReadAsync())
                    {
                        InspectionEquipment equipment =
                            new InspectionEquipment();

                        equipment.EquipmentName =
                            reader["EquipmentName"] as string;

                        equipment.EquipmentSerialNo =
                            reader["EquipmentSerialNo"] as string;

                        if (reader["CalibrationDate"] != DBNull.Value)
                            equipment.CalibrationDate =
                                Convert.ToDateTime(reader["CalibrationDate"]);

                        if (reader["CalibrationDueDate"] != DBNull.Value)
                            equipment.CalibrationDueDate =
                                Convert.ToDateTime(reader["CalibrationDueDate"]);

                        report.InspectionEquipments.Add(equipment);
                    }
                }
            }

            return report;
        }

        public List<InspectionReport> GetAllReports()
        {
            return _context.InspectionReports
              .OrderBy(x => x.InspectionReportId)
              .ToList();
        }
        public async Task<byte[]> GeneratePdfAsync(int id, bool? sp)
        {
            InspectionReport report = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (sp==true)
            {
                //Get Report Data using StoredProcedure
                report =
               await GetReportDataFromSpAsync(id);
            }
            else
            {
                //Get Report Data Using EF
                report = await GetReportDataAsync(id);
            }
            stopwatch.Stop();

            long dataFetchTime = stopwatch.ElapsedMilliseconds;

            Debug.WriteLine(
                "========================================");

            Debug.WriteLine(
                "Data Fetch Method: " +
                (sp==true ? "Stored Procedure" : "Entity Framework"));

            Debug.WriteLine("Time taken " + dataFetchTime + "MS");


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