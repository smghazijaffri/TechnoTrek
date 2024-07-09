using Dapper;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using SharedClass.Components.Model;

namespace SharedClass.Components.Data
{
    public class Email : EmailCredentials
    {
        private readonly Connection con = new();
        public Select select;

        public async Task GetEmailAsync(string RfqNumber)
        {
            using SqlConnection db = new(con.connectionString);
            var emails = db.Query<Vendor>("SELECT Email, VendorName FROM RFQVendor r INNER JOIN Vendor v ON r.VendorID = v.VendorID WHERE SendEmail = 1 AND RFQNumber = @RFQNumber", new { RFQNumber = RfqNumber }).ToList();

            foreach (var email in emails)
            {
                SendModel sendModel = new()
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
                byte[] pdfBytes = await select.GetPdfAsync("Request for Quotation", sendModel.RFQNumber);

                var attachment = new Attachment(new MemoryStream(pdfBytes), "Request for Quotation.pdf");
                message.Attachments.Add(attachment);

                using var client = new SmtpClient(Emailhost, EmailPort);
                client.Credentials = new NetworkCredential(UserName, Password);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetEmailBody(string name)
        {
            string body = "Dear " + name + "<br/><br/>Please find the attached Request For Quotation Document.<br/><br/>TechnoTrek";
            return body;
        }
    }
}