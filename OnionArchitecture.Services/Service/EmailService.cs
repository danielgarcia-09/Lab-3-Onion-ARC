using Microsoft.Extensions.Options;
using OnionArchitecture.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Services.Service
{
    public class Email
    {
        public string From { get; set; }

        public string FromEmail { get; set; }

        public string To { get; set; }

        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }

    public interface IEmailService
    {
        void SendEmail(IOptions<Smtp> appSettings, Email email);
    }
    public class EmailService : IEmailService
    {

        public void SendEmail(IOptions<Smtp> appSettings, Email email)
        {

            var smtpInfo = appSettings.Value;

            SmtpClient smtpClient = new SmtpClient(smtpInfo.Host)
            {
                Port = smtpInfo.Port,
                Credentials = new NetworkCredential(smtpInfo.Username, smtpInfo.Password),
                EnableSsl = true, 
            };

            MailAddress from = new MailAddress(email.FromEmail,email.From);

            MailAddress to = new MailAddress(email.ToEmail, email.To);

            MailMessage mailMessage = new MailMessage(from, to);

            mailMessage.Body = email.Message;

            mailMessage.Subject = email.Subject;

            smtpClient.Send(mailMessage);
        }
    }
}
