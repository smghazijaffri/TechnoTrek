﻿using Dapper;
using Microsoft.Data.SqlClient;
using SharedClass.Components.Pages.CustomPC;
using SharedClass.Components.Model;
using static MudBlazor.CategoryTypes;

namespace SharedClass.Components.Data
{
    public class Select : Update
    {
        private readonly SqlConnection con;

        public Select()
        {
            con = GetSqlConnection();
        }

        public async Task<IEnumerable<Motherboard>> GetMotherboardsAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM motherboards";
            return await con.QueryAsync<Motherboard>(query);
        }

        public async Task<IEnumerable<Processor>> GetProcessorsAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM processors";
            return await con.QueryAsync<Processor>(query);
        }

        public async Task<IEnumerable<GPU>> GetGPUAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM graphics_cards";
            return await con.QueryAsync<GPU>(query);
        }

        public async Task<IEnumerable<Case>> GetCaseAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM gaming_cases";
            return await con.QueryAsync<Case>(query);
        }

        public async Task<IEnumerable<Memory>> GetMemoryAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM memory";
            return await con.QueryAsync<Memory>(query);
        }

        public async Task<IEnumerable<Storage>> GetStorageAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM storage";
            return await con.QueryAsync<Storage>(query);
        }

        public async Task<IEnumerable<PSU>> GetPSUAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM power_supplies";
            return await con.QueryAsync<PSU>(query);
        }

        public async Task<IEnumerable<Cooler>> GetCoolerAsync()
        {
            con.Close();
            con.Open();
            string query = "SELECT * FROM coolers";
            return await con.QueryAsync<Cooler>(query);
        }

        public async Task<IEnumerable<PurchaseRequisition>> GetPR1DataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<PurchaseRequisition>("SELECT * FROM PurchaseRequest");
        }

        public async Task<IEnumerable<PurchaseOrders>> GetPODataAsync()
        {
            con.Close();
            con.Open();
            return await con.QueryAsync<PurchaseOrders>("SELECT * FROM PurchaseOrder");
        }

        public List<PurchaseRequisition> PurhcaseRequest(string PRNumber)
        {
            con.Close(); con.Open();
            return con.Query<PurchaseRequisition>("select * from PurchaseRequest where PRnumber = @PRnumber", new { PRnumber = PRNumber }).ToList();
        }

        public int CountPRnumber()
        {
            con.Close(); con.Open();
            return con.QueryFirstOrDefault<int>("Select COUNT(1)PRnumber from PurchaseRequest");
        }

        public List<PurchaseOrders> PurhcaseOrderNumber(string PONumber)
        {
            con.Close();
            con.Open();
            return con.Query<PurchaseOrders>("select * from PurchaseOrder where POnumber = @POnumber", new { POnumber = PONumber }).ToList();
        }

        public int CountPOnumber()
        {
            con.Close(); con.Open();
            return con.QueryFirstOrDefault<int>("Select COUNT(1)POnumber from PurchaseOrder");
        }
    }
}