using Microsoft.EntityFrameworkCore;
using Tuto.Domain.Models;

namespace Tuto.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TeacherInfo> TeacherInfos { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
