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
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Subject> Subjects{ get; set; }
        public DbSet<TeacherSubject> TeacherSubjects{ get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(x => x.UserId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Role>().Property(x => x.RoleId).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<TeacherInfo>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ChatMessage>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Lesson>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Review>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Region>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<City>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Subject>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<TeacherSubject>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
