using System.Threading.Tasks;
using MPI_Report.Models;
using System.Collections.Generic;
namespace MPI_Report.Services.Interfaces
{
    public interface IInspectionReportService
    {
        Task<byte[]> GeneratePdfAsync(int id);
        List<InspectionReport> GetAllReports();
    }
}