using CryptoTrackerApp.Classes;
using Microsoft.VisualBasic.ApplicationServices;
using Supabase.Gotrue;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public partial class EmailService
{
    private string smtpServer = "smtp-relay.brevo.com";
    private int port = 587;
    private string login = "78cd77001@smtp-brevo.com";
    private string password = "P9yG3FULqRhx5fJW";
    private string fromEmail = "grupops36@gmail.com";
    private string fromName = "Crypto Tracker App";
    private string subject = "Alert from Crypto Tracker App";

    public async Task SendEmailAsync(string toEmail, string toName, string plainTextContent, string htmlContent)
    {
        var fromAddress = new MailAddress(fromEmail, fromName);
        var toAddress = new MailAddress(toEmail, toName);

        using (var smtp = new SmtpClient
        {
            Host = smtpServer,
            Port = port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(login, password)
        })
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = plainTextContent,
                IsBodyHtml = true,
            })
            {
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextContent, null, "text/plain"));
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlContent, null, "text/html"));

                try
                {
                    await smtp.SendMailAsync(message);
                    MessageBox.Show("Correo enviado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar el correo: " + ex.Message);
                }
            }
        }
    }
}