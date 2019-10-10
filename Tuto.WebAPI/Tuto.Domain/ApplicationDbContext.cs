using Microsoft.EntityFrameworkCore;
using Tuto.Domain.Models;

namespace Tuto.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
