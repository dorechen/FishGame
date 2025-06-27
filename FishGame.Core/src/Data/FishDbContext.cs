using Microsoft.EntityFrameworkCore;
using FishGame.Models;

namespace FishGame.Core.Data
{
    public class FishDbContext : DbContext
    {
        public DbSet<Fish> Fish { get; set; }

        public string DbPath { get; private set; }

        public FishDbContext()
        {
            // Store database in the application's local directory
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "fishgame.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fish>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastUpdate).IsRequired();
                entity.Property(e => e.Color).IsRequired();
                entity.Property(e => e.X).IsRequired();
                entity.Property(e => e.Y).IsRequired();
                entity.Property(e => e.isFacingRight).IsRequired();
                entity.Property(e => e.fullness).IsRequired();
            });
        }
    }
} 