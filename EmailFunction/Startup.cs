using EmailFunction.Model;
using EmailFunction.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(EmailFunction.Startup))]
namespace EmailFunction
{
    public class Startup : FunctionsStartup
    {
        IConfiguration Configuration;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;

            string email = Configuration["EmailSettings-Email"];
            string password = Configuration["EmailSettings-Password"];
            string host = Configuration["EmailSettings-Host"];
            string displayName = Configuration["EmailSettings-DisplayName"];

            var emailSettings = new EmailSettings(displayName, host, email, password);

            builder.Services.AddSingleton<IEmailService>(new EmailService(emailSettings));
        }
    }
}
