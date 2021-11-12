using Buy_Sell_Board.Models;
using Buy_Sell_Board.Models.Announcement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buy_Sell_Board.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Subcategory> Subcategorys { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Image> Images { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(50);
            builder.Entity<AppUser>()
                .Property(e => e.LastName)
                .HasMaxLength(50);
            builder.Entity<AppUser>()
                .Property(e => e.Date_of_Birth)
                .HasMaxLength(50);
            builder.Entity<AppUser>()
                .Property(e => e.Region)
                .HasMaxLength(50);
            builder.Entity<AppUser>()
                .Property(e => e.City)
                .HasMaxLength(50);
        }
    }
}
