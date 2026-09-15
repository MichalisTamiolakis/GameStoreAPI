namespace GameStore.Api.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Use decimal instead of float, to prevent small float precision errors accumulating over time.
        public decimal Price { get; set; }

        public string Genre { get; set;  } = string.Empty;
    }
}
