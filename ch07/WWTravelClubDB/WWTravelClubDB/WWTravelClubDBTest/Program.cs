using WWTravelClubDB;
using WWTravelClubDB.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("program start: populate database, press a key to continue");
Console.ReadKey();

var context = new LibraryDesignTimeDbContextFactory()
    .CreateDbContext();

// First database entry, inserting data
// var firstDestination = new Destination
// {
//     Name = "Florence",
//     Country = "Italy",
//     Packages = new List<Package>()
//     {
//         new Package
//         {
//             Name = "Summer in Florence",
//             StartValidityDate = new DateTime(2019, 6, 1),
//             EndValidityDate = new DateTime(2019, 10, 1),
//             DurationInDays=7,
//             Price=1000
//         },
//         new Package
//         {
//             Name = "Winter in Florence",
//             StartValidityDate = new DateTime(2019, 12, 1),
//             EndValidityDate = new DateTime(2020, 2, 1),
//             DurationInDays=7,
//             Price=500
//         }
//     }
// };

// context.Destinations.Add(firstDestination);
// await context.SaveChangesAsync();

// Console.WriteLine($"DB populated: first destination id is {firstDestination.Id}");
// Console.ReadKey();


// Second database entry, modify data
var toModify = await context.Destinations
    .Where(m => m.Name == "Florence")
    .Include(m => m.Packages)
    .FirstOrDefaultAsync();

toModify.Description = "Florence is a famous historical Italian town";

foreach (var package in toModify.Packages)
    package.Price = package.Price * 1.1m;

await context.SaveChangesAsync();

var verifyChanges = await context.Destinations
    .Where(m => m.Name == "Florence")
    .FirstOrDefaultAsync();

Console.WriteLine($"New Florence description: {verifyChanges.Description}");
Console.ReadKey();