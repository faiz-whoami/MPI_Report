using System.Security.Cryptography;
using System.Text;

namespace MPI_Report.Infrastructure
{
    public static class PasswordHasher
    {
        private const string Prefix = "MPI|";

        public static string Hash(string password)
        {
            if (password == null)
            {
                password = string.Empty;
            }

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Prefix + password);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            string computed = Hash(password);
            return string.Equals(computed, storedHash, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
