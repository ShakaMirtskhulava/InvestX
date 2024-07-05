using GHotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GHotel.Persistance.Context;

public class AppDBContext : DbContext
{
    private readonly ConnectionStrings _connectionStrings;

    public DbSet<Business>? Businesses { get; set; }
    public DbSet<Project>? Projects { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Share>? Shares { get; set; }
    public DbSet<MyImage>? Images { get; set; }

    public AppDBContext(IOptions<ConnectionStrings> connectionStringsOptions)
    {
        _connectionStrings = connectionStringsOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_connectionStrings.DefaultConnection);
            //.UseSqlServer("Server=tcp:hackathon.ct6o8ioae98u.eu-central-1.rds.amazonaws.com,1433;Initial Catalog=NewDatabase;User ID=admin;Password=hackathon123;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Business>()
            .HasMany(b => b.Projects)
            .WithOne(p => p.Business)
            .HasForeignKey(pr => pr.BusinessId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .Property(p => p.RequiredBudget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Project>()
            .Property(p => p.CurrentBudget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Business>()
            .HasOne(bu => bu.User)
            .WithMany(us => us.Businesses)
            .HasForeignKey(bu => bu.UserPersonalNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Share>()
            .HasOne(sh => sh.Project)
            .WithMany(pr => pr.Shares)
            .HasForeignKey(sh => sh.ProjectName)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Share>()
            .HasOne(sh => sh.User)
            .WithMany(us => us.Shares)
            .HasForeignKey(sh => sh.UserPersonalNumber)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasMany(ro => ro.Images)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Business>()
            .HasOne(bu => bu.Image)
            .WithOne()
            .HasForeignKey<Business>(bu => bu.ImageUrl)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Transaction>()
            .HasOne(tr => tr.Project)
            .WithMany(pr => pr.Transactions)
            .HasForeignKey(tr => tr.ProjectName)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(tr => tr.User)
            .WithMany(pr => pr.Transactions)
            .HasForeignKey(tr => tr.UserPersonalNumber)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
