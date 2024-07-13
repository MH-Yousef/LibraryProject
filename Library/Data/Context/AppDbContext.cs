using Core.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=45.84.189.34\\MSSQLSERVER2019;Database=ragnarzz_library;User Id=ragnarzz_ragnar;Password=@304ii1oY;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(builder => builder.SentFriendships).WithOne(builder => builder.Sender).HasForeignKey(builder => builder.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ApplicationUser>().HasMany(builder => builder.ReceivedFriendships).WithOne(builder => builder.Receiver).HasForeignKey(builder => builder.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Section>().HasMany(builder => builder.Books).WithOne(builder => builder.Section).HasForeignKey(builder => builder.SectionId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Section>().HasMany(builder => builder.Shelves).WithOne(builder => builder.Section).HasForeignKey(builder => builder.SectionId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Section>().HasMany(builder => builder.Categories).WithOne(builder => builder.Section).HasForeignKey(builder => builder.SectionId).OnDelete(DeleteBehavior.NoAction);

            // books
            builder.Entity<Book>().HasOne(builder => builder.Shelf).WithMany(builder => builder.Books).HasForeignKey(builder => builder.ShelfId).OnDelete(DeleteBehavior.NoAction);

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
    }
}
