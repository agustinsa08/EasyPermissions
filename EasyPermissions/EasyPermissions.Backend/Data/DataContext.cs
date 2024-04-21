using Microsoft.EntityFrameworkCore;
using EasyPermissions.Shared.Entities;

namespace EasyPermissions.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<CategoryNotice> CategoryNotices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TypeNotice> TypeNotices { get; set; }
        public DbSet<Notice> Notices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Area>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<CategoryNotice>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            modelBuilder.Entity<TypeNotice>().HasIndex(x => new { x.CategoryNoticeId, x.Name }).IsUnique();
            modelBuilder.Entity<Notice>().HasIndex(x => new { x.CategoryNoticeId, x.Name }).IsUnique();
            modelBuilder.Entity<ImageNotice>().HasIndex(x => new { x.NoticeId, x.Name }).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
