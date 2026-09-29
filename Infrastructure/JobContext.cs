using System.Web;

namespace MPI_Report.Infrastructure
{
    public static class JobContext
    {
        public const string JobIdKey = "CurrentJobId";
        public const string JobNoKey = "CurrentJobNo";
        public const string CustomerIdKey = "CurrentCustomerId";
        public const string RigIdKey = "CurrentRigId";

        public static void Set(HttpSessionStateBase session, int jobId, string jobNo, int customerId, int rigId)
        {
            session[JobIdKey] = jobId;
            session[JobNoKey] = jobNo;
            session[CustomerIdKey] = customerId;
            session[RigIdKey] = rigId;
        }

        public static int? GetJobId(HttpSessionStateBase session)
        {
            if (session == null || session[JobIdKey] == null)
            {
                return null;
            }

            return (int)session[JobIdKey];
        }

        public static string GetJobNo(HttpSessionStateBase session)
        {
            if (session == null || session[JobNoKey] == null)
            {
                return string.Empty;
            }

            return session[JobNoKey].ToString();
        }

        public static void Clear(HttpSessionStateBase session)
        {
            session.Remove(JobIdKey);
            session.Remove(JobNoKey);
            session.Remove(CustomerIdKey);
            session.Remove(RigIdKey);
        }
    }
}
