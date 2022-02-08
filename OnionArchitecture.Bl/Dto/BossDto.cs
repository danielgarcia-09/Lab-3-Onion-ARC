using System.Collections.Generic;

namespace OnionArchitecture.Bl.Dto
{
    public class BossDto : BaseDto
    {
        public List<EmployeeDto> Employees { get; set; }
    }
}