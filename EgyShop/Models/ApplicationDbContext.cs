using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgyShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreComments> StoreComments { get; set; }
        public DbSet<StoreType> StoreType { get; set; }
        public DbSet<Category> StoreCategory { get; set; }
        public DbSet<Product> CategoryProducts { get; set; }

        public DbSet<StoreThemeSetting> StoreThemeSettings { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                   .HasOne(a => a.Store)
                   .WithOne(b => b.User)
                   .HasForeignKey<Store>(b => b.UserID);

            builder.Entity<Store>()
                   .HasOne(a => a.StoreThemeSetting)
                   .WithOne(a => a.Store)
                   .HasForeignKey<StoreThemeSetting>( t => t.StoreId);
                   
        }

    }
}
