using Microsoft.Data.SqlClient;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        private readonly string connectionString = "Data Source=DESKTOP-GVLTI9D\\SQLEXPRESS;Initial Catalog=Example;Encrypt=False;Integrated Security=True";

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
