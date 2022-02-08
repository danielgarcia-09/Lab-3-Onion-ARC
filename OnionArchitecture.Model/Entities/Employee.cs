using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Model.Entities
{
    public class Employee : BaseEntity
    {
        public int BossId { get; set; }

        public Boss Boss { get; set; }
    }
}
