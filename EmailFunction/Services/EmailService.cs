using EmailFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailFunction.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService( EmailSettings emailSettings )
        {
            _emailSettings = emailSettings;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            var networkCredential = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            var client = new SmtpClient(_emailSettings.Host)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = networkCredential,
                Port = 587,
                EnableSsl = true
            };

            var mailAddress = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName);

            var mailMessage = new MailMessage()
            {
                Subject = "Prueba de Azure Function",
                SubjectEncoding = Encoding.UTF8,
                From = mailAddress,
                Body = emailModel.Message,
                BodyEncoding = Encoding.UTF8
            };

            foreach (var destinatary in emailModel.Destinataries)
            {
                mailMessage.To.Add(new MailAddress(destinatary));
            }

            client.Send(mailMessage);

            return true;
        }
    }

   
}
