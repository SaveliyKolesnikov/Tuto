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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(x => x.UserId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Role>().Property(x => x.RoleId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<TeacherInfo>().Property(x => x.TeacherInfoId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ChatMessage>().Property(x => x.ChatMessageId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Lesson>().Property(x => x.LessonId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Review>().Property(x => x.ReviewId).HasDefaultValueSql("NEWID()");
        }
    }
}
