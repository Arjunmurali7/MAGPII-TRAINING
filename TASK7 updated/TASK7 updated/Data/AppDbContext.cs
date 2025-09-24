using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data 
{
    public class AppDbContext : DbContext // dbcontext inhert from efcore dbcontext connection string provider
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // constructor with optionss to connect to db

        public DbSet<Employee> Employees { get; set; } // DbSet for Employees table employee model to tableinsql

    }
}
