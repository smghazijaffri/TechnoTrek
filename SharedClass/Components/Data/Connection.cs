using Microsoft.Data.SqlClient;

namespace SharedClass.Components.Data
{
    public class Connection
    {
        private readonly string connectionString = "Data Source=192.168.100.97,1433;Initial Catalog=Example;User Id=Ghazi;Password=fC5y2qRU;Encrypt=False;";

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}