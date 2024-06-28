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

            using (SqlConnection db = new(connectionString))
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
                parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                db.Execute(SP, parameters, commandType: commandType);

                outputValue = parameters.Get<string>("@Output");
            }

            return outputValue;
        }

        public OutputClass CRD2(dynamic Model, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false, bool outputMessage = false, bool errorMessage = false)
        {
            OutputClass output = new();

            using (SqlConnection db = new(connectionString))
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
                    parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }
                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }

                db.Execute(SP, parameters, commandType: CommandType.StoredProcedure);

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
            OutputClass output = new();

            using (SqlConnection db = new(connectionString))
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
                    parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }
                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }

                db.Execute(SP, parameters, commandType: CommandType.StoredProcedure);

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

        public OutputClass CRD4(DynamicParameters parameters, string SP, CommandType commandType = CommandType.StoredProcedure, bool errorMessage = false)
        {
            OutputClass output = new();

            using (SqlConnection db = new(connectionString))
            {
                parameters.Add("@CreationDate", DateTime.Now);

                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }

                var result = db.Query(SP, parameters, commandType: commandType).ToList();
                output.Data = result;

                if (errorMessage == true)
                {
                    output.ErrorMessage = parameters.Get<string>("@ErrorMessage");
                }
            }

            return output;
        }
    }
}