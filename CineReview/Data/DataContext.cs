using CineReview.Models;
using Microsoft.EntityFrameworkCore;

namespace CineReview.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Serie> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>().ToTable("Media");

            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Serie>().ToTable("Series");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>().HasKey(ac => new { ac.MediaId, ac.UserId });
            modelBuilder.Entity<Review>()
                        .HasOne(ac => ac.Media)
                        .WithMany(a => a.Reviews)
                        .HasForeignKey(ac => ac.MediaId);
            modelBuilder.Entity<Review>()
                        .HasOne(ac => ac.User)
                        .WithMany(c => c.Reviews)
                        .HasForeignKey(ac => ac.UserId);
        }
    }
}
