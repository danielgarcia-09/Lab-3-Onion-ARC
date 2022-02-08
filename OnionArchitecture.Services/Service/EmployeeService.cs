using AutoMapper;
using Microsoft.Extensions.Options;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Core.Model;
using OnionArchitecture.Model.Context;
using OnionArchitecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Services.Service
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeDto, CompanyContext>
    {
        Task<Email[]> UpdateBossEmail(int id, int bossId);
    }
    public class EmployeeService : BaseService<Employee,EmployeeDto, CompanyContext>, IEmployeeService
    {
        private readonly IBossService _bossService;
        public EmployeeService(IBossService service, CompanyContext context, IMapper mapper ) : base( context, mapper )
        {
            _bossService = service;
        }

        public async Task<Email[]> UpdateBossEmail(int id, int bossId)
        {
            var dto = await GetById(id);

            if( dto is not null )
            {
                var oldBoss = await _bossService.GetById(dto.BossId);

                dto.BossId = bossId;
                var updated = await Update(id, dto);

               if( updated is not null )
                {
                    var newBoss = await _bossService.GetById(updated.BossId);

                    var toOldBoss = new Email
                    {
                        From = "Daniel Garcia",
                        FromEmail = "daniel.garcia7913@gmail.com",
                        To = oldBoss.Name + " " + oldBoss.LastName,
                        ToEmail = oldBoss.Email,
                        Subject = "Boss Change",
                        Message = $"Next week {updated.Name + " " + updated.LastName} will be employee of {newBoss.Name + " " + newBoss.LastName}, sorry for the inconveniences."
                    };

                    var toNewBoss = new Email
                    {
                        From = "Daniel Garcia",
                        FromEmail = "daniel.garcia7913@gmail.com",
                        To = newBoss.Name + " " + newBoss.LastName,
                        ToEmail = newBoss.Email,
                        Subject = "Boss Change",
                        Message = $"Next week {updated.Name + " " + updated.LastName} will be your new employee because he was having some troubles with his/her old boss and chose you, sorry for the sudden email."
                    };

                    return new Email[] { toOldBoss, toNewBoss };
                }
                return null;
            }
            return null;
        }
    }
}
