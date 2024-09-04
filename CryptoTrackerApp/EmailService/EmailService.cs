using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using NLog;

public class EmailService : IEmailService
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    private readonly string smtpServer;
    private readonly int port;
    private readonly string login;
    private readonly string password;
    private readonly string fromEmail;
    private readonly string fromName;

    public EmailService(IConfiguration configuration)
    {
        smtpServer = configuration["SmtpConfig:Server"];
        port = int.Parse(configuration["SmtpConfig:Port"]);
        login = configuration["SmtpConfig:Login"];
        password = configuration["SmtpConfig:Password"];
        fromEmail = configuration["SmtpConfig:FromEmail"];
        fromName = configuration["SmtpConfig:FromName"];
    }

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
                Subject = "Alert from Crypto Tracker App",
                Body = plainTextContent,
                IsBodyHtml = true,
            })
            {
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextContent, null, "text/plain"));
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlContent, null, "text/html"));

                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Failed to send email.");
                    throw;
                }
            }
        }
    }
}