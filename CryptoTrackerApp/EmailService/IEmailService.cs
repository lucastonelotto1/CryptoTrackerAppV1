using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrackerApp.EmailService
{
        public interface IEmailService
        {
            Task SendEmailAsync(string toEmail, string toName, string plainTextContent, string htmlContent);
        }
}
