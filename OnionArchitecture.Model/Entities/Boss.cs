using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Model.Entities
{
    public class Boss : BaseEntity
    {
        public ICollection<Employee> Employees{ get; set; }
    }
}
