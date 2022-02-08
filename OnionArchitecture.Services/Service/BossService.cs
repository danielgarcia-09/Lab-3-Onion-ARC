using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Model.Context;
using OnionArchitecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Services.Service
{
    public interface IBossService : IBaseService<Boss, BossDto, CompanyContext>
    {
        Task<List<BossDto>> GetBosses();
        Task<BossDto> GetBoss(int id);
    }
    public class BossService : BaseService<Boss, BossDto, CompanyContext>, IBossService
    {
        public BossService(CompanyContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<BossDto>> GetBosses()
        {
            var bosses = await Get();
            return bosses;
        }
        public async Task<BossDto> GetBoss(int id)
        {
            var dto = await GetById(id);

            var employees = await _context.Employees.Where(x => x.BossId.Equals(id)).ToListAsync();

            dto.Employees = _mapper.Map<List<EmployeeDto>>(employees);

            return dto;
        }
    }
}
