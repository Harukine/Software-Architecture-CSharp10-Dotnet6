using WWTravelClubDB;
using WWTravelClubDB.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("program start: populate database, press a key to continue");
Console.ReadKey();

var context = new LibraryDesignTimeDbContextFactory()
    .CreateDbContext();

var firstDestination = new Destination
{
    Name = "Florence",
    Country = "Italy",
    Packages = new List<Package>()
    {
        new Package
        {
            Name = "Summer in Florence",
            StartValidityDate = new DateTime(2019, 6, 1),
            EndValidityDate = new DateTime(2019, 10, 1),
            DurationInDays=7,
            Price=1000
        },
        new Package
        {
            Name = "Winter in Florence",
            StartValidityDate = new DateTime(2019, 12, 1),
            EndValidityDate = new DateTime(2020, 2, 1),
            DurationInDays=7,
            Price=500
        }
    }
};

context.Destinations.Add(firstDestination);
await context.SaveChangesAsync();

Console.WriteLine($"DB populated: first destination id is {firstDestination.Id}");
Console.ReadKey();