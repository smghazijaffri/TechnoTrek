using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SharedClass.Components.Data
{
    public class CRUD : Connection
    {
        public string CRD(dynamic Model, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false)
        {
            string outputValue = string.Empty;

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

                var parameters = new DynamicParameters(Model);
                parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 50); // Assuming output parameter size is 50

                db.Execute(SP, parameters, commandType: commandType);

                // Retrieve the output parameter value
                outputValue = parameters.Get<string>("@Output");
            }

            return outputValue;
        }
    }
}