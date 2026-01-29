using Finoku.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finoku.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}


        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().Property(a => a.Amount).HasPrecision(18, 4);
            modelBuilder.Entity<Asset>().Property(a => a.PurchasePrice).HasPrecision(18,4);
            modelBuilder.Entity<AssetCategory>().ToTable("AssetCategory");


            // User - Asset Bağlantısı
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assets)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Category)
                .WithMany()
                .HasForeignKey("AssetCategoryId");

            // Kategoriler 
            modelBuilder.Entity<AssetCategory>().HasData(
                new AssetCategory { Id = 1, Name = "Cash" },
                new AssetCategory { Id = 2, Name = "Stocks" },
                new AssetCategory { Id = 3, Name = "Cryptocurrency" },
                new AssetCategory { Id = 4, Name = "Gold" }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "123", Role = UserRole.Admin },
                new User { Id = 2, Username = "user", Password = "123", Role = UserRole.Regular }
            );
        }
    }
}
