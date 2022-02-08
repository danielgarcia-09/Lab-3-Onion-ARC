using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Model.Entities;
using OnionArchitecture.Services.Service;
using System.Threading.Tasks;

namespace OnionArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController<T, TDto, TContext> : ControllerBase 
        where T : BaseEntity
        where TDto : BaseDto
        where TContext : DbContext
    {
        private readonly IBaseService<T,TDto,TContext> _service;
        public BaseController(IBaseService<T, TDto, TContext> service)
        {
            _service = service;
        } 

        [HttpGet]
        public virtual async Task<IActionResult> Get() {
            return Ok(await _service.Get());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetById(id);

            if( dto is not null )
            {
                return Ok(dto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create( TDto dto )
        {
            var newDto = await _service.Create(dto);

            if (newDto is not null)
            {
                return Ok(newDto);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update( int id, TDto dto )
        {
            var updatedDto = await _service.Update(id,dto);

            if (updatedDto is not null)
            {
                return Ok(updatedDto);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete( int id )
        {
            var isDeleted = await _service.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
