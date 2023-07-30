using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace WWTravelClubDB
{
    public class LibraryDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<MainDbContext>
    {
        private const string connectionString =
        @"Server=localhost;Database=wwtravelclub;User Id=SA;Password=P@ssw0rd;Trusted_Connection=False;TrustServerCertificate=True";
        public MainDbContext CreateDbContext(params string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();

            builder.UseSqlServer(connectionString);
            return new MainDbContext(builder.Options);
        }
    }
}