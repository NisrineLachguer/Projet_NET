using MimeKit;
using MailKit.Net.Smtp;
using System;

public static class EmailSender
{
    public static void SendEmailWithPdfAttachment(string recipientEmail, byte[] pdfContent)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Lachguer_emsi", "nisrinelachguer2@gmail.com"));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = "Employee and Client List PDF";

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = "Please find the attached PDF file containing the employee and client information.";
        
        // Attach the PDF
        bodyBuilder.Attachments.Add("Employee_Client_List.pdf", pdfContent);

        message.Body = bodyBuilder.ToMessageBody();

        // Sending email using Gmail's SMTP
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("nisrinelachguer2@gmail.com", "Nisrine2003@@@");

            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}