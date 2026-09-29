using MPI_Report.Models;

namespace MPI_Report.Services.Interfaces
{
    public interface IAccountService
    {
        User Validate(string username, string password);
    }
}
