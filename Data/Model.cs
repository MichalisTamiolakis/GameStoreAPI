namespace GameStore.Api.Data
{
    using GameStore.Api.Models;
    using Microsoft.EntityFrameworkCore;

    public class GameStoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set;  }


        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>(e =>{
                e.Property(g => g.Name).HasMaxLength(100);
                e.Property(g => g.Genre).HasMaxLength(50);
                e.Property(g => g.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.Property(u => u.Name).HasMaxLength(100);
                e.Property(u => u.Surname).HasMaxLength(100);
                e.Property(u => u.DisplayName).HasMaxLength(32);
                e.Property(u => u.Email).HasMaxLength(254);
                e.HasIndex(u => u.Email);
            });
        }
    }
}
