using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;

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

        public OutputClass CRD3(DynamicParameters parameters, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false, bool outputMessage = false, bool errorMessage = false, bool Remainingamount = false )
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
                parameters.Add("@UserID", UserIDSession.UserID);

                if (outputMessage == true)
                {
                    parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }
                if (errorMessage == true)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }
                if (Remainingamount == true)
                {
                    parameters.Add("@Remainingamount", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 2000);

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
                if (Remainingamount == true)
                {
                    output.RemainingAmount = parameters.Get<int>("@Remainingamount");
                }
            }

            return output;
        }

        public async Task<OutputClass> CRD4(DynamicParameters parameters, string sp, CommandType commandType = CommandType.StoredProcedure, bool errorMessage = false)
        {
            OutputClass output = new();

            using (SqlConnection db = new(connectionString))
            {
                parameters.Add("@CreationDate", DateTime.Now);

                if (errorMessage)
                {
                    parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                }

                var result = (await db.QueryAsync(sp, parameters, commandType: commandType)).ToList();
                output.Data = result;

                if (errorMessage)
                {
                    output.ErrorMessage = parameters.Get<string>("@ErrorMessage");
                }
            }

            return output;
        }
        public static int CRDPOS(ProductModel Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }
                
            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32 (UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.AddDynamicParams(Model);
                ObjParm.Add("@Id", Model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                var res = db.Execute("Evs_Sp_Product2", ObjParm, commandType: CommandType.StoredProcedure);
                Model.Id = ObjParm.Get<int>("@Id");
                return res;
            }
        }
        public static int VariantsStock(ProductVariantsStock Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                return db.Execute("Evs_Sp_Product_Variants_Stock", Model, commandType: CommandType.StoredProcedure);
            }
        }
        public static int Variants(ProductVariants Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.AddDynamicParams(Model);
                ObjParm.Add("@Id", Model.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                var res = db.Execute("Evs_Sp_Product_Variants", ObjParm, commandType: CommandType.StoredProcedure);
                Model.Id = ObjParm.Get<int>("@Id");
                return res;
            }
        }
        public static int Location(ProductLocation Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                return db.Execute("Evs_Sp_Product_Location", Model, commandType: CommandType.StoredProcedure);
            }
        }
        public static int VariantsPrice(ProductVariantsPrice Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                return db.Execute("Evs_Sp_Product_Variants_Price", Model, commandType: CommandType.StoredProcedure);
            }
        }
        public static int VariantsStockHistory(ProductVariantsStockHistory Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                return db.Execute("Evs_Sp_Product_Variants_Stock_History", Model, commandType: CommandType.StoredProcedure);
            }
        }
        public static int Tax(ProductTax Model, bool IsDelete = false)
        {
            Model.Insert = 1;
            if (IsDelete) { Model.Insert = 0; }

            Model.ModifiedOn = DateTime.Now;
            Model.ModifiedBy = Convert.ToInt32(UserIDSession.UserID);
            if (Model.Id == 0)
            {
                Model.CreatedBy = Convert.ToInt32(UserIDSession.UserID);
                Model.CreatedOn = DateTime.Now;
            }
            using (SqlConnection db = new(connectionStringPOS))
            {
                return db.Execute("Evs_Sp_Product_Tax", Model, commandType: CommandType.StoredProcedure);
            }
        }
        public bool CheckPermission(int permission)
        {
            if (UserIDSession.PermissionList.Contains(permission))
            {
                return true;
            }   
            else
            {
                return false;
            }
        }
    }
}