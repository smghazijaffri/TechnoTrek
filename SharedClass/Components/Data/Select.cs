using static SharedClass.Components.Pages.CustomPC.Motherboard;
using SharedClass.Components.Model;
using Microsoft.Data.SqlClient;
using PdfSharpCore.Pdf.IO;
using BoldReports.Writer;
using System.Reflection;
using PdfSharpCore.Pdf;
using System.Text.Json;
using BoldReports.Web;
using System.Data;
using Dapper;
using System.Text;
using System.Security.Cryptography;

namespace SharedClass.Components.Data
{
    public class Select : CRUD
    {
        private readonly SqlConnection con;

        public Select()
        {
            con = GetSqlConnection();
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync()
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items ORDER BY ItemCode");
        }

        public async Task<IEnumerable<ItemClass>> GetItemsAsync(string ItemType)
        {
            return await con.QueryAsync<ItemClass>("SELECT * FROM Items WHERE ItemType = @ItemType ORDER BY ItemCode", new { ItemType });
        }

        public static DataTable ConvertListToDataTable(List<string> list)
        {
            DataTable table = new();
            table.Columns.Add("Value", typeof(string));

            foreach (string s in list)
            {
                table.Rows.Add(s);
            }

            return table;
        }

        public static DataTable ConvertIntToDataTable(List<int> list)
        {
            DataTable table = new();
            table.Columns.Add("RowID", typeof(int));

            foreach (int s in list)
            {
                table.Rows.Add(s);
            }

            return table;
        }

        public async Task<IEnumerable<Stock>> GetStockDataAsync(string ItemID = null)
        {
            var query = "SELECT * FROM Stock";

            if (ItemID != null)
            {
                query += " WHERE ItemID = @ItemID";
            }

            query += " ORDER BY ItemID";

            return await con.QueryAsync<Stock>(query, new { ItemID });
        }

        public static bool IsValidJson(string input)
        {
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) || (input.StartsWith("[") && input.EndsWith("]")))
            {
                try
                {
                    JsonDocument.Parse(input);
                    return true;
                }
                catch (JsonException)
                {
                    return false;
                }
            }
            return false;
        }

        public static DataTable ConvertToDataTable<T>(List<T> list)
        {
            DataTable table = new();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo property in properties)
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ItemTable()
        {
            DataTable table = new();
            table.Columns.Add("RowID", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UOM", typeof(string));
            table.Columns.Add("Rate", typeof(string));
            table.Columns.Add("Amount", typeof(int));
            table.Columns.Add("RequiredBy", typeof(DateTime));
            return table;
        }

        public static DataTable PRItemTable()
        {
            DataTable ItemTable = new();
            ItemTable.Columns.Add("RowID", typeof(int));
            ItemTable.Columns.Add("Item", typeof(string));
            ItemTable.Columns.Add("Quantity", typeof(int));
            ItemTable.Columns.Add("UOM", typeof(string));
            ItemTable.Columns.Add("RequiredBy", typeof(DateTime));
            return ItemTable;
        }

        public static DataTable VendorTable()
        {
            DataTable table = new();
            table.Columns.Add("VendorID", typeof(string));
            table.Columns.Add("RowID", typeof(string));
            table.Columns.Add("SendEmail", typeof(bool));
            return table;

        }

        public byte[] GetPdfAsync(string ReportName, string? ID = null, DateTime? from = null, DateTime? to = null)
        {
            DynamicParameters parameters = new();
            parameters.Add("@ReportName", ReportName);
            if (!string.IsNullOrEmpty(ID)) parameters.Add("@ID", ID);
            if (!string.IsNullOrEmpty(from.ToString())) parameters.Add("@StartDate", from);
            if (!string.IsNullOrEmpty(to.ToString())) parameters.Add("@EndDate", to);

            var output = CRD4(parameters, "GetReportData", CommandType.StoredProcedure, errorMessage: true);
            List<dynamic> reportData = output.Data;

            if (reportData == null || reportData.Count == 0)
            {
                throw new Exception("No data found for the specified report.");
            }

            using MemoryStream inputStream = new(con.QuerySingleOrDefault<byte[]>("SELECT RDLData FROM Reports WHERE ReportName = @ReportName", new { ReportName }) ?? throw new Exception("Report not found in the database."));
            ReportWriter writer = new(inputStream);

            writer.DataSources.Add(new ReportDataSource("DataSet1", reportData));

            using MemoryStream memoryStream = new();
            writer.Save(memoryStream, WriterFormat.PDF);

            return memoryStream.ToArray();
        }

        public static byte[] ExtractOddPages(byte[] pdfBytes)
        {
            using var inputDocument = PdfReader.Open(new MemoryStream(pdfBytes), PdfDocumentOpenMode.Import);
            using var outputDocument = new PdfDocument();

            for (int i = 0; i < inputDocument.PageCount; i++)
            {
                if (i % 2 == 0)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                }
            }

            using var outputStream = new MemoryStream();

            outputDocument.Save(outputStream);
            return outputStream.ToArray();
        }

        public static async Task OpenPdfAsync(byte[] pdfBytes, string fileName)
        {
            string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
            await File.WriteAllBytesAsync(filePath, pdfBytes);
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }

        public static async Task<List<Bulk>> RetrieveCartItems(string userid)
        {
            List<Bulk> cartItems = [];
            CRUD crud = new();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userid);
                parameters.Add("@Action", "Retrieve");
                parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                var result = await Task.Run(() => crud.CRD3(parameters, "ManageCartItems", CommandType.StoredProcedure, false, outputMessage: true, errorMessage: true));

                if (!string.IsNullOrEmpty(result.Output))
                {
                    if (IsValidJson(result.Output))
                    {
                        cartItems = JsonSerializer.Deserialize<List<Bulk>>(result.Output);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return cartItems;
        }

        public static async Task<bool> CheckCompatibilityAsync(SqlConnection db, string itemCode, List<string> compatItems)
        {
            DataTable compiItem = ConvertListToDataTable(compatItems);
            var parameters = new DynamicParameters();
            parameters.Add("@ItemCode", itemCode);
            parameters.Add("@CompatibilityID", compiItem.AsTableValuedParameter("dbo.CompatibleItems"));
            parameters.Add("@Output", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            await db.ExecuteAsync("item_Compatibility", parameters, commandType: CommandType.StoredProcedure);
            string outputValue = parameters.Get<string>("@Output");
            return outputValue == "Allow";
        }

        public static string HashPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return "SHA256$" + builder.ToString();
        }
    }
}
