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

