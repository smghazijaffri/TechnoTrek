using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace SharedClass.Components.Data
{
    public class CRUD : Login
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

        public OutputClass CRD2(dynamic Model, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false, bool outputMessage = false, bool errorMessage = false)
        {
            OutputClass output = new OutputClass();

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
                if (outputMessage == true)
                {
                    parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000); // Assuming output parameter size is 50
                }
                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000); // Assuming output parameter size is 50
                }

                db.Execute(SP, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                if (outputMessage == true)
                {
                    output.Output = parameters.Get<string>("@Output");
                }
                if (errorMessage == true)
                {
                    output.ErrorMessage = parameters.Get<string>("@ErrorMessage");
                }
            }

            return output;
        }

        public OutputClass CRD3(DynamicParameters parameters, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false, bool outputMessage = false, bool errorMessage = false)
        {
            OutputClass output = new OutputClass();

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                if (IsDelete)
                {
                    parameters.Add("@ForInsert", 0);

                }
                else
                {
                    parameters.Add("@ForInsert", 1);

                }
                parameters.Add("@CreationDate", DateTime.Now);

                if (outputMessage == true)
                {
                    parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000); // Assuming output parameter size is 50
                }
                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000); // Assuming output parameter size is 50
                }

                db.Execute(SP, parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the output parameter value
                if (outputMessage == true)
                {
                    output.Output = parameters.Get<string>("@Output");
                }
                if (errorMessage == true)
                {
                    output.ErrorMessage = parameters.Get<string>("@ErrorMessage");
                }
            }

            return output;
        }
    }
}