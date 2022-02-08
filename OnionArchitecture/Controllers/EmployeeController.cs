using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Core.Model;
using OnionArchitecture.Model.Context;
using OnionArchitecture.Model.Entities;
using OnionArchitecture.Services.Service;
using System.Threading.Tasks;

namespace OnionArchitecture.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeDto, CompanyContext>
    {
        private readonly IEmployeeService _service;

        private readonly IEmailService _emailService;

        private readonly IOptions<Smtp> appSettings;
        public EmployeeController( 
            IEmployeeService service,
            IEmailService emailservice, 
            IOptions<Smtp> app 
            ) : base( service )
        {
            _service = service;
            _emailService = emailservice;
            appSettings = app;
        }

        [HttpPatch("{id}/{bossId}")]
        public async Task<IActionResult> ChangeBoss (int id, int bossId)
        {
            var emails = await _service.UpdateBossEmail(id, bossId);

            if(emails is not null)
            {
                _emailService.SendEmail(appSettings, emails[0]);
                _emailService.SendEmail(appSettings, emails[1]);
                return Ok();
            }
            return BadRequest();
        } 
    }
}
