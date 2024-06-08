﻿using EasyPermissions.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyPermissions.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.SetCommandTimeout(600);
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<CategoryNotice> CategoryNotices { get; set; }
        public DbSet<CategoryPermission> CategoryPermissions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ImageNotice> ImageNotices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionDetail> PermissionDetails { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TypeNotice> TypeNotices { get; set; }
        public DbSet<TypePermission> TypePermissions { get; set; }
        public DbSet<Notice> Notices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Area>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<CategoryNotice>().HasIndex(x => new { x.TypeNoticeId, x.Name }).IsUnique();
            modelBuilder.Entity<CategoryPermission>().HasIndex(x => new { x.TypePermissionId, x.Name }).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            modelBuilder.Entity<TypeNotice>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<TypePermission>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Notice>().HasIndex(x => new { x.CategoryNoticeId, x.Name }).IsUnique();
            modelBuilder.Entity<ImageNotice>().HasIndex(x => new { x.NoticeId, x.Name }).IsUnique();
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.User)
                .WithMany(u => u.Permissions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
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