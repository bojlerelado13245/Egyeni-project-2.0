using Microsoft.EntityFrameworkCore;
using MyApp.Backend.Models;
namespace MyApp.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //itt adom hozza a tablat
    public DbSet<Users> Users { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
}