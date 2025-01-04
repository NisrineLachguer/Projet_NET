using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

public static class EmailSender
{
    private const string SMTP_HOST = "smtp.gmail.com";
    private const int SMTP_PORT = 587;
    private const string SENDER_EMAIL = "nisrinelachguer2@gmail.com";
    // Store this securely in configuration, not in code
    private const string APP_PASSWORD = ""; 

    public static async Task SendEmailWithPdfAttachmentAsync(string recipientEmail, byte[] pdfContent)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Lachguer_emsi", SENDER_EMAIL));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "Employee and Client List PDF";

            var builder = new BodyBuilder();
            builder.TextBody = "Please find the attached PDF file containing the employee and client information.";
            builder.Attachments.Add("Employee_Client_List.pdf", pdfContent);

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(SMTP_HOST, SMTP_PORT, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(SENDER_EMAIL, APP_PASSWORD);
                //await client.SendMailAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}", ex);
        }
    }
}