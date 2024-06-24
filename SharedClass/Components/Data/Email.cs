using Dapper;
using System.Net;
using System.Net.Mail;
using BoldReports.Web;
using BoldReports.Writer;
using System.Data.SqlClient;
using SharedClass.Components.Model;

namespace SharedClass.Components.Data
{
    public class Email : EmailCredentials
    {
        private readonly Connection con = new();

        public async Task GetEmailAsync(string RfqNumber)
        {
            using SqlConnection db = new SqlConnection(con.connectionString);
            var emails = db.Query<Vendor>("SELECT Email, VendorName FROM RFQVendor r INNER JOIN Vendor v ON r.VendorID = v.VendorID WHERE SendEmail = 1 AND RFQNumber = @RFQNumber", new { RFQNumber = RfqNumber }).ToList();

            foreach (var email in emails)
            {
                SendModel sendModel = new SendModel
                {
                    Body = GetEmailBody(email.VendorName),
                    To = email.Email,
                    Subject = "Request for Quotation",
                    RFQNumber = RfqNumber
                };
                await SendEmailAsync(sendModel);
                await Task.Delay(1000);
            }
        }

        private async Task SendEmailAsync(SendModel sendModel)
        {
            try
            {
                using var message = new MailMessage(UserName, sendModel.To);
                message.Subject = sendModel.Subject;
                message.Body = sendModel.Body;
                message.IsBodyHtml = true;
                byte[] attachmentBytes = ExportToPDF(sendModel.RFQNumber);
                var attachment = new Attachment(new MemoryStream(attachmentBytes), "RFQ.pdf");
                message.Attachments.Add(attachment);

                using var client = new SmtpClient(Emailhost, EmailPort);
                client.Credentials = new NetworkCredential(UserName, Password);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public byte[] ExportToPDF(string RFQNumber)
        {
            using SqlConnection db = new(con.connectionString);

            byte[] rdlData = db.QuerySingleOrDefault<byte[]>("SELECT RDLData FROM Reports WHERE ReportName = @ReportName", new { ReportName = "RFQ" });

            if (rdlData == null)
            {
                throw new Exception("Report not found in the database.");
            }

            using MemoryStream inputStream = new MemoryStream(rdlData);
            ReportWriter writer = new ReportWriter(inputStream);

            var rfqItems = db.Query<RFQItemReport>(
                "SELECT RFQNumber, ItemName AS Item, Quantity, UOMName AS UOM, FORMAT(RequiredBy, 'yyyy-MM-dd') AS RequiredBy " +
                "FROM RFQ_Items r INNER JOIN Items i ON r.Item = i.ItemCode INNER JOIN UOM u ON r.UOM = u.UOMID " +
                "WHERE RFQNumber = @RFQNumber", new { RFQNumber = RFQNumber }).ToList();
            writer.DataSources.Add(new ReportDataSource("DataSet1", rfqItems));

            using MemoryStream memoryStream = new MemoryStream();
            writer.Save(memoryStream, WriterFormat.PDF);

            return memoryStream.ToArray();
        }

        public async Task<byte[]> GetPdfAsync(string RFQNumber)
        {
            using SqlConnection db = new(con.connectionString);

            byte[] rdlData = await db.QuerySingleOrDefaultAsync<byte[]>("SELECT RDLData FROM Reports WHERE ReportName = @ReportName", new { ReportName = "RFQ" });

            if (rdlData == null)
            {
                throw new Exception("Report not found in the database.");
            }

            using MemoryStream inputStream = new(rdlData);
            ReportWriter writer = new(inputStream);

            var rfqItems = (await db.QueryAsync<RFQItemReport>(
                "SELECT RFQNumber, ItemName AS Item, Quantity, UOMName AS UOM, FORMAT(RequiredBy, 'yyyy-MM-dd') AS RequiredBy " +
                "FROM RFQ_Items r INNER JOIN Items i ON r.Item = i.ItemCode INNER JOIN UOM u ON r.UOM = u.UOMID " +
                "WHERE RFQNumber = @RFQNumber", new { RFQNumber })).ToList();
            writer.DataSources.Add(new ReportDataSource("DataSet1", rfqItems));

            using MemoryStream memoryStream = new();
            writer.Save(memoryStream, WriterFormat.PDF);

            return memoryStream.ToArray();
        }

        private static string GetEmailBody(string name)
        {
            string body = "Dear " + name + "<br/><br/>Please find the attached Request For Quotation Document.<br/><br/>TechnoTrek";
            return body;
        }
    }
}