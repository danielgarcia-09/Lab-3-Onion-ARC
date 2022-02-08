using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailService
{
    public class EmailModel
    {
        public string Message { get; set; }

        public List<string> Destinataries { get; set; }
    }

    public class EmailService
    {
        [Function("EmailSender")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("EmailSender");
            logger.LogInformation("Se enviara un email");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var emailModel = JsonConvert.DeserializeObject<EmailModel>(requestBody);
            
            var emailSent = await SendEmail(emailModel);

            var response = emailSent ? req.CreateResponse(HttpStatusCode.OK) : req.CreateResponse(HttpStatusCode.BadRequest);

            return response;
        }
        public async Task<bool> SendEmail(EmailModel emailModel) 
        {
            var networkCredential = new NetworkCredential("daniel.garcia7913@gmail.com", "garushia09");
            var client = new SmtpClient("smtp.gmail.com")
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = networkCredential,
                Port = 587,
                EnableSsl = true
            };

            var mailAddress = new MailAddress("daniel.garcia7913@gmail.com", "Daniel Mercedes");

            var mailMessage = new MailMessage()
            {
                Subject = "Prueba de Azure Function",
                SubjectEncoding = System.Text.Encoding.UTF8,
                From = mailAddress,
                Body = emailModel.Message,
                BodyEncoding = System.Text.Encoding.UTF8
            };

            foreach(var destinatary in emailModel.Destinataries)
            {
                mailMessage.To.Add(new MailAddress(destinatary));
            }

            client.Send(mailMessage);
            
            return true;
        }
    }
}
