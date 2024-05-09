using EduOne.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduOne.DataAccess;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EduOneDb;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
}
