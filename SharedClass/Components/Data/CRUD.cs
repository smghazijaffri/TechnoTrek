using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using SharedClass.Components.Data;
using SharedClass.Components.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace SharedClass.Components.Data
{

    public  class CRUD: Connection
    {
        private readonly SqlConnection con;
        public CRUD()
        {
            con = GetSqlConnection();
        }
        public void CRD(dynamic Model, string SP, CommandType commandType = CommandType.StoredProcedure, bool IsDelete = false)
        {
            try
            {
                con.Close();
                con.Open();
                if (IsDelete)
                {
                    Model.ForInsert = 0;
                }
                else
                {
                    Model.ForInsert = 1;
                }
                Model.CreationDate = DateTime.Now;
                con.Execute(SP, (object)Model, commandType: commandType);
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }

    }
}

