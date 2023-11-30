using Dapper;
using Microsoft.Data.SqlClient;
using SharedClass.Components.Pages.CustomPC;

namespace SharedClass.Components.Data
{
    public class Select : Update
    {
        public async Task<IEnumerable<Motherboard>> GetMotherboardsAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM motherboards";
                return await con.QueryAsync<Motherboard>(query);
            }
        }
        public async Task<IEnumerable<Processor>> GetProcessorsAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM processors";
                return await con.QueryAsync<Processor>(query);
            }
        }
        public async Task<IEnumerable<GPU>> GetGPUAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM graphics_cards";
                return await con.QueryAsync<GPU>(query);
            }
        }
        public async Task<IEnumerable<Case>> GetCaseAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM gaming_cases";
                return await con.QueryAsync<Case>(query);
            }
        }
        public async Task<IEnumerable<Memory>> GetMemoryAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM memory";
                return await con.QueryAsync<Memory>(query);
            }
        }
        public async Task<IEnumerable<Storage>> GetStorageAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM storage";
                return await con.QueryAsync<Storage>(query);
            }
        }
        public async Task<IEnumerable<PSU>> GetPSUAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM power_supplies";
                return await con.QueryAsync<PSU>(query);
            }
        }
        public async Task<IEnumerable<Cooler>> GetCoolerAsync()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                con.Open();
                // Execute a SQL query using Dapper
                string query = "SELECT * FROM coolers";
                return await con.QueryAsync<Cooler>(query);
            }
        }
    }
}
