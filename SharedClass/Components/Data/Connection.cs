using Microsoft.Data.SqlClient;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        private readonly string connectionString = "Server=192.168.100.1,1433;Data Source=DESKTOP-6JM2045\\SQLEXPRESS;Initial Catalog=Example;Encrypt=false;Integrated Security=True;Trust Server Certificate=True";

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
