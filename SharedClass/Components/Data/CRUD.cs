using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SharedClass.Components.Data
{
    public class CRUD : Connection
    {
        public void CRD(dynamic Model, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                if (IsDelete)
                {
                    Model.ForInsert = 0;
                }
                else
                {
                    Model.ForInsert = 1;
                }
                Model.CreationDate = DateTime.Now;
                db.Execute(SP, (object)Model, commandType: commandType);
            }
        }
    }
}