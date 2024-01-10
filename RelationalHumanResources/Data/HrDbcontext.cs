using Microsoft.EntityFrameworkCore;
using RelationalHumanResources.Models;

namespace RelationalHumanResources.Data
{
    public class HrDbcontext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SalaryHistory> SalariesHistory { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=(local);Initial Catalog=relationalHrDb-2024; TrustServerCertificate=True;Integrated Security = True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        public HrDbcontext()
        {
        }

        public HrDbcontext(DbContextOptions<HrDbcontext> options)
       : base(options)
        {
        }


    }
}
