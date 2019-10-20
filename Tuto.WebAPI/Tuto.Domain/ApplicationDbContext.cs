using Microsoft.EntityFrameworkCore;
using System;
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
            modelBuilder.Entity<User>().Property(x => x.UserId).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<User>().Property(x => x.Picture).HasConversion(v => v.ToString(), v => new Uri(v));
            modelBuilder.Entity<Role>().Property(x => x.RoleId).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<TeacherInfo>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<ChatMessage>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<Lesson>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<Review>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<Region>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<City>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<Subject>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<TeacherSubject>().Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}
