using System;
using Microsoft.EntityFrameworkCore;
using WWTravelClubDB.Models;

namespace WWTravelClubDB;

public class MainDbContext : DbContext
{
    public DbSet<Package> Packages { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public MainDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Configuration mapping for Destination
        builder.Entity<Destination>()
            .HasMany(m => m.Packages)
            .WithOne(m => m.MyDestination)
            .HasForeignKey(m => m.DestinationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration mapping for Package
        builder.Entity<Package>()
            .HasOne(m => m.MyDestination)
            .WithMany(m => m.Packages)
            .HasForeignKey(m => m.DestinationId)
            .OnDelete(DeleteBehavior.Cascade);
        // Extra settings for Package
        builder.Entity<Package>()
            .Property(m => m.Price)
            .HasPrecision(10, 3);

        // Apply Extra Destination from Config class
        new DestinationConfiguration()
                .Configure(builder.Entity<Destination>());
        // Apply Extra Package from Config class
        new PackageConfiguration()
            .Configure(builder.Entity<Package>());
    }
}