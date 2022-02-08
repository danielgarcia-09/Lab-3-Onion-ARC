using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Model.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext( DbContextOptions<CompanyContext> options ) : base(options)
        {

        } 
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Boss> Bosses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Employee>()
                .Property(p => p.Salary)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Boss>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Boss>()
                .Property(p => p.Salary)
                .HasPrecision(10, 3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
