using System;
using Microsoft.EntityFrameworkCore;

namespace WWTravelClubDB;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions options) : base(options)
    {

    }
}

