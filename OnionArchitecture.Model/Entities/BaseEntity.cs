using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Model.Entities
{
    public interface IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public decimal Salary { get; set; }

        public bool Deleted { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public decimal Salary { get; set; }

        public bool Deleted { get; set; }
    }
}
