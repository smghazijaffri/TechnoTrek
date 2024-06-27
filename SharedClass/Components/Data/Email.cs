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
                byte[] attachmentBytes = GetPdfAsync(sendModel.RFQNumber);
                var attachment = new Attachment(new MemoryStream(attachmentBytes), "Request for Quotation.pdf");
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

        public byte[] GetPdfAsync(string RFQNumber)
        {
            using SqlConnection db = new(con.connectionString);

            byte[] rdlData = db.QuerySingleOrDefault<byte[]>("SELECT RDLData FROM Reports WHERE ReportName = @ReportName", new { ReportName = "Request for Quotation" }) ?? throw new Exception("Report not found in the database.");
            using MemoryStream inputStream = new(rdlData);
            ReportWriter writer = new(inputStream);

            var rfqItems = (db.Query<RFQItemReport>(
                "SELECT ROW_NUMBER() OVER (ORDER BY i.ItemName) AS No, r.RFQNumber, ItemName AS Item, Quantity, UOMName AS UOM, FORMAT(RequiredBy, 'yyyy-MM-dd') AS RequiredBy, FORMAT(rq.DocumentDate, 'yyyy-MM-dd') AS DocumentDate " +
                "FROM RFQ_Items r INNER JOIN Items i ON r.Item = i.ItemCode INNER JOIN UOM u ON r.UOM = u.UOMID INNER JOIN RequestForQuotation rq ON r.RFQNumber = rq.RFQNumber " +
                "WHERE r.RFQNumber = @RFQNumber", new { RFQNumber })).ToList();
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