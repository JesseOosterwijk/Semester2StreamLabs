using System.Data.SqlClient;

namespace Data
{
    class Connection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                @"Data Source=mssql.fhict.local;Initial Catalog=dbi398785;User ID=dbi398785;Password=hodehallofbiman;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
