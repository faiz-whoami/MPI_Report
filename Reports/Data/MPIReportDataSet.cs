using System;
using System.Data;
using MPI_Report.Models;
using InspectionReportModel = MPI_Report.Models.InspectionReport;
namespace MPI_Report.Reports.Data
{
    public class MPIReportDataSet
    {
        public DataSet DataSet { get; private set; }

        public MPIReportDataSet()
        {
            DataSet = new DataSet("MPIReportDataSet");

            CreateReportHeaderTable();
            CreateEquipmentTable();
            CreateConsumablesTable();
            CreateTestEvaluationTable();
        }

        // ============================================================
        // CREATE TABLE STRUCTURES
        // ============================================================

        private void CreateReportHeaderTable()
        {
            DataTable table = new DataTable("ReportHeader");

            table.Columns.Add("InspectionReportId", typeof(int));
            table.Columns.Add("WorkOrderNo", typeof(string));
            table.Columns.Add("ReportNo", typeof(string));
            table.Columns.Add("InspectionDate", typeof(DateTime));
            table.Columns.Add("RecommendedDueDate", typeof(DateTime));

            table.Columns.Add("CustomerName", typeof(string));
            table.Columns.Add("TestLocation", typeof(string));

            table.Columns.Add("ItemDescription", typeof(string));
            table.Columns.Add("ItemSerialNo", typeof(string));
            table.Columns.Add("Manufacturer", typeof(string));
            table.Columns.Add("RatingSWL", typeof(string));
            table.Columns.Add("ModelNo", typeof(string));
            table.Columns.Add("ServiceType", typeof(string));
            table.Columns.Add("InspectionTypeCategory", typeof(string));
            table.Columns.Add("Material", typeof(string));

            table.Columns.Add("SurfaceCondition", typeof(string));
            table.Columns.Add("LiftingCapacity", typeof(string));
            table.Columns.Add("SurfaceTemperature", typeof(decimal));
            table.Columns.Add("Sensitivity", typeof(string));

            table.Columns.Add("TestMethod", typeof(string));
            table.Columns.Add("LightingMethod", typeof(string));
            table.Columns.Add("MagnetismType", typeof(string));
            table.Columns.Add("CurrentType", typeof(string));
            table.Columns.Add("MagneticFieldDirection", typeof(string));
            table.Columns.Add("ProcedureNo", typeof(string));

            table.Columns.Add("AcceptanceCriteria", typeof(string));
            table.Columns.Add("LightingType", typeof(string));

            table.Columns.Add("InspectedItemImagePath", typeof(string));

            table.Columns.Add("Comments", typeof(string));
            table.Columns.Add("AreaOfTesting", typeof(string));
            table.Columns.Add("RestrictedAccess", typeof(string));
            table.Columns.Add("InspectionRemarks", typeof(string));
            table.Columns.Add("InspectionResult", typeof(string));

            table.Columns.Add("InspectorName", typeof(string));
            table.Columns.Add("InspectorQualification", typeof(string));
            table.Columns.Add("InspectorSignaturePath", typeof(string));
            table.Columns.Add("InspectorSignedDate", typeof(DateTime));

            table.Columns.Add("ReviewerName", typeof(string));
            table.Columns.Add("ReviewerDesignation", typeof(string));
            table.Columns.Add("ReviewerSignaturePath", typeof(string));
            table.Columns.Add("ReviewedDate", typeof(DateTime));

            DataSet.Tables.Add(table);
        }

        private void CreateEquipmentTable()
        {
            DataTable table = new DataTable("Equipment");

            table.Columns.Add("InspectionEquipmentId", typeof(int));
            table.Columns.Add("InspectionReportId", typeof(int));
            table.Columns.Add("EquipmentName", typeof(string));
            table.Columns.Add("EquipmentSerialNo", typeof(string));
            table.Columns.Add("CalibrationDate", typeof(DateTime));
            table.Columns.Add("CalibrationDueDate", typeof(DateTime));

            DataSet.Tables.Add(table);
        }

        private void CreateConsumablesTable()
        {
            DataTable table = new DataTable("Consumables");

            table.Columns.Add("InspectionConsumableId", typeof(int));
            table.Columns.Add("InspectionReportId", typeof(int));
            table.Columns.Add("ConsumableName", typeof(string));
            table.Columns.Add("BrandType", typeof(string));

            DataSet.Tables.Add(table);
        }

        private void CreateTestEvaluationTable()
        {
            DataTable table = new DataTable("TestEvaluation");

            table.Columns.Add("TestEvaluationId", typeof(int));
            table.Columns.Add("InspectionReportId", typeof(int));
            table.Columns.Add("JointNo", typeof(string));
            table.Columns.Add("WelderId", typeof(string));
            table.Columns.Add("WeldLength", typeof(string));
            table.Columns.Add("Discontinuity", typeof(string));
            table.Columns.Add("StartLocation", typeof(string));
            table.Columns.Add("EndLocation", typeof(string));
            table.Columns.Add("DefectLength", typeof(string));
            table.Columns.Add("Evaluation", typeof(string));

            DataSet.Tables.Add(table);
        }

        // ============================================================
        // POPULATE DATASET
        // ============================================================

        public void Build(InspectionReportModel report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            PopulateReportHeader(report);
            PopulateEquipment(report);
            PopulateConsumables(report);
            PopulateTestEvaluations(report);
        }

        // ============================================================
        // REPORT HEADER
        // ============================================================

        private void PopulateReportHeader(InspectionReportModel report)
        {
            DataTable table = DataSet.Tables["ReportHeader"];

            DataRow row = table.NewRow();

            row["InspectionReportId"] = report.InspectionReportId;
            row["WorkOrderNo"] = DbValue(report.WorkOrderNo);
            row["ReportNo"] = DbValue(report.ReportNo);

            row["InspectionDate"] = report.InspectionDate;
            row["RecommendedDueDate"] =
                DbValue(report.RecommendedDueDate);

            row["CustomerName"] =
                DbValue(report.Customer != null
                    ? report.Customer.Name
                    : null);

            row["TestLocation"] = DbValue(report.TestLocation);

            row["ItemDescription"] =
                DbValue(report.ItemDescription);

            row["ItemSerialNo"] =
                DbValue(report.ItemSerialNo);

            row["Manufacturer"] =
                DbValue(report.Manufacturer);

            row["RatingSWL"] =
                DbValue(report.RatingSWL);

            row["ModelNo"] =
                DbValue(report.ModelNo);

            row["ServiceType"] =
                DbValue(report.ServiceType);

            row["InspectionTypeCategory"] =
                DbValue(report.InspectionTypeCategory);

            row["Material"] =
                DbValue(report.Material);

            row["SurfaceCondition"] =
                DbValue(report.SurfaceCondition);

            row["LiftingCapacity"] =
                DbValue(report.LiftingCapacity);

            row["SurfaceTemperature"] =
                DbValue(report.SurfaceTemperature);

            row["Sensitivity"] =
                DbValue(report.Sensitivity);

            row["TestMethod"] =
                DbValue(report.TestMethod);

            row["LightingMethod"] =
                DbValue(report.LightingMethod);

            row["MagnetismType"] =
                DbValue(report.MagnetismType);

            row["CurrentType"] =
                DbValue(report.CurrentType);

            row["MagneticFieldDirection"] =
                DbValue(report.MagneticFieldDirection);

            row["ProcedureNo"] =
                DbValue(report.ProcedureNo);

            row["AcceptanceCriteria"] =
                DbValue(report.AcceptanceCriteria);

            row["LightingType"] =
                DbValue(report.LightingType);

            row["InspectedItemImagePath"] =
                DbValue(report.InspectedItemImagePath);

            row["Comments"] =
                DbValue(report.Comments);

            row["AreaOfTesting"] =
                DbValue(report.AreaOfTesting);

            row["RestrictedAccess"] =
                DbValue(report.RestrictedAccess);

            row["InspectionRemarks"] =
                DbValue(report.InspectionRemarks);

            row["InspectionResult"] =
                DbValue(report.InspectionResult);

            row["InspectorName"] =
                DbValue(report.InspectorName);

            row["InspectorQualification"] =
                DbValue(report.InspectorQualification);

            row["InspectorSignaturePath"] =
                DbValue(report.InspectorSignaturePath);

            row["InspectorSignedDate"] =
                DbValue(report.InspectorSignedDate);

            row["ReviewerName"] =
                DbValue(report.ReviewerName);

            row["ReviewerDesignation"] =
                DbValue(report.ReviewerDesignation);

            row["ReviewerSignaturePath"] =
                DbValue(report.ReviewerSignaturePath);

            row["ReviewedDate"] =
                DbValue(report.ReviewedDate);

            table.Rows.Add(row);
        }

        // ============================================================
        // EQUIPMENT
        // ============================================================

        private void PopulateEquipment(InspectionReportModel report)
        {
            DataTable table = DataSet.Tables["Equipment"];

            foreach (InspectionEquipment equipment
                in report.InspectionEquipments)
            {
                DataRow row = table.NewRow();

                row["InspectionEquipmentId"] =
                    equipment.InspectionEquipmentId;

                row["InspectionReportId"] =
                    equipment.InspectionReportId;

                row["EquipmentName"] =
                    DbValue(equipment.EquipmentName);

                row["EquipmentSerialNo"] =
                    DbValue(equipment.EquipmentSerialNo);

                row["CalibrationDate"] =
                    DbValue(equipment.CalibrationDate);

                row["CalibrationDueDate"] =
                    DbValue(equipment.CalibrationDueDate);

                table.Rows.Add(row);
            }
        }

        // ============================================================
        // CONSUMABLES
        // ============================================================

        private void PopulateConsumables(InspectionReportModel report)
        {
            DataTable table = DataSet.Tables["Consumables"];

            foreach (InspectionConsumable consumable
                in report.InspectionConsumables)
            {
                DataRow row = table.NewRow();

                row["InspectionConsumableId"] =
                    consumable.InspectionConsumableId;

                row["InspectionReportId"] =
                    consumable.InspectionReportId;

                row["ConsumableName"] =
                    DbValue(consumable.ConsumableName);

                row["BrandType"] =
                    DbValue(consumable.BrandType);

                table.Rows.Add(row);
            }
        }

        // ============================================================
        // TEST EVALUATIONS
        // ============================================================

        private void PopulateTestEvaluations(InspectionReportModel report)
        {
            DataTable table = DataSet.Tables["TestEvaluation"];

            foreach (TestEvaluation evaluation
                in report.TestEvaluations)
            {
                DataRow row = table.NewRow();

                row["TestEvaluationId"] =
                    evaluation.TestEvaluationId;

                row["InspectionReportId"] =
                    evaluation.InspectionReportId;

                row["JointNo"] =
                    DbValue(evaluation.JointNo);

                row["WelderId"] =
                    DbValue(evaluation.WelderId);

                row["WeldLength"] =
                    DbValue(evaluation.WeldLength);

                row["Discontinuity"] =
                    DbValue(evaluation.Discontinuity);

                row["StartLocation"] =
                    DbValue(evaluation.StartLocation);

                row["EndLocation"] =
                    DbValue(evaluation.EndLocation);

                row["DefectLength"] =
                    DbValue(evaluation.DefectLength);

                row["Evaluation"] =
                    DbValue(evaluation.Evaluation);

                table.Rows.Add(row);
            }
        }

        // ============================================================
        // NULL HANDLING
        // ============================================================

        private object DbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}
