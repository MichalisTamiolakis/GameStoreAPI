namespace GameStore.Api.Data
{
    using GameStore.Api.Models;
    using Microsoft.EntityFrameworkCore;

    public class GameStoreContext : DbContext
    {
        public DbSet<Game> Games { get; set;  }

        public GameStoreContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GameStoreDB;Trusted_Connection=True;");
        }

    }
}
