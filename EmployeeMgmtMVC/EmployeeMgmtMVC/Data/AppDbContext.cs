using Microsoft.EntityFrameworkCore;
using EmployeeMgmtMVC.Models;

namespace EmployeeMgmtMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
