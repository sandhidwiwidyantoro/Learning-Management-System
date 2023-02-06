using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Principal;

namespace API.Contexts;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }

    // Introduces the Model to the Database that eventually becomes an Entity
    public DbSet<BatchClass> BatchClass { get; set; }
    public DbSet<Materi> Materi{ get; set; }
    public DbSet<Participant> Participant { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<ParticipantTugas> ParticipantTugas { get; set; }
    public DbSet<Tugas> Tugas{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Email);
        
        foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}
