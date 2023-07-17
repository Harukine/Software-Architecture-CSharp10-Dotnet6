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

    }
}