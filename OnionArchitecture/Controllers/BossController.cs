using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Model.Context;
using OnionArchitecture.Model.Entities;
using OnionArchitecture.Services.Service;
using System.Threading.Tasks;

namespace OnionArchitecture.Controllers
{
    
    public class BossController : BaseController<Boss, BossDto, CompanyContext>
    {
        private readonly IBossService _service;
        public BossController(IBossService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public async override Task<IActionResult> Get()
        {
            var boss = await _service.GetBosses();
            if( boss is not null )
            {
                return Ok(boss);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async override Task<IActionResult> GetById(int id)
        {
            var bosses = await _service.GetBoss(id);
            if (bosses is not null)
            {
                return Ok(bosses);
            }
            return NotFound();
        }

    }
}
