using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tuto.Domain
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}Tuto.API", Path.DirectorySeparatorChar);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath).AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
            return new ApplicationDbContext(builder.Options);
        }
    }
}
