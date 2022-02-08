using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Services.IoC
{
    public static class ServiceRegistry
    {
        public static void AddServiceRegistry( this IServiceCollection services )
        {
            services.AddTransient<IBossService, BossService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
