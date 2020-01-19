using System.Data.SqlClient;

namespace Data
{
    public class Connection
    {
        internal SqlConnection conn { get; }

        public Connection(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }
    }
}
