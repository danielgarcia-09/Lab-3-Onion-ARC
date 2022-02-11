using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EmailFunction.Services;
using EmailFunction.Model;

namespace EmailFunction
{
    public class EmailFunction
    {
        private readonly IEmailService _service;

        public EmailFunction( IEmailService service )
        {
            _service = service;
        }

        [FunctionName("SendEmail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Sending Email..........");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var emailModel = JsonConvert.DeserializeObject<EmailModel>(requestBody);

            var emailSent = await _service.SendEmail(emailModel);

            if (emailSent) return new OkObjectResult(emailSent);

            return new BadRequestObjectResult(emailSent);            
        }
    }
}
