using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperSeller.Models;

namespace SuperSeller.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Ads)
                .WithOne(a => a.Creator)
                .HasForeignKey(a => a.CreatorId);

            builder
                .Entity<User>()
                .HasMany(u => u.ObservedAds)
                .WithOne(oa => oa.User)
                .HasForeignKey(oa => oa.UserId);

            builder
                .Entity<Ad>()
                .HasMany(a => a.Viewers)
                .WithOne(v => v.Ad)
                .HasForeignKey(v => v.AdId);

            builder
                .Entity<Ad>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Ad)
                .HasForeignKey(p => p.AdId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
