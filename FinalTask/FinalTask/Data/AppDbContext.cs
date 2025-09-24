using EmployeeAttendanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceAPI.Data
{
    public class AppDbContext : DbContext // appdb inherit from DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }// constructor to initialize the dbcontext with options like sql connections

        public DbSet<Attendance> Attendances { get; set; }// dbset for attendance records in the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)// method to configure the model how your classes map to the database.
        {
            modelBuilder.Entity<Attendance>() // configure the Attendance entity
                .HasIndex(a => new { a.EmployeeId, a.Date })// create a composite index on EmployeeId and Date
                .IsUnique(); // Employee cannot have two records for same day
        }
    }
}
