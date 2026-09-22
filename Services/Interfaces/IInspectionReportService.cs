using System.Threading.Tasks;

namespace MPI_Report.Services.Interfaces
{
    public interface IInspectionReportService
    {
        Task<byte[]> GeneratePdfAsync(int id);
    }
}