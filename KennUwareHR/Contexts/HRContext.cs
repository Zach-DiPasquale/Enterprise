using System;
using KennUwareHR.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Linq;

namespace KennUwareHR.Contexts
{
    public class HRContext : DbContext
    {

        public HRContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Paycheck> Paychecks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
