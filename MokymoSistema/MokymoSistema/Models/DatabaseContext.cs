using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MokymoSistema.Models;

public class DatabaseContext : IdentityDbContext<User>
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
}